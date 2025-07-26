using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ControllHealthPoint : MonoBehaviour
{
    public characterStat stat;
    public float flashTime = 1f;
    public AnimationCurve flashCurve;
    public GameObject effectDamage;

    public bool useGameManager;

    private State stateH = State.Idle; //Состояние здоровья игрока

    private GameObject gameManager;
    private float currentHealthPoint;
    private GameObject healthBar;
    private Material material;
    private Coroutine _hitDamageEffect;
    private enum State
    {
        Idle,
        ContinuosDamage
    }

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar");
        material = GetComponent<SpriteRenderer>().material;
    }

    //Изменеение размера полоски в bar
    public void ChangeHealthBar() 
    {
        currentHealthPoint = (float)(stat.healthPoint) / stat.maxHealthPoint;
        healthBar.GetComponent<Image>().fillAmount = currentHealthPoint;
    }

    //Востановление
    public void Recovery(int _recoveryPoint)
    {
        stat.healthPoint = stat.healthPoint + _recoveryPoint;
        ChangeHealthBar();
    }

    //Полное восстановление
    public void FullRecovery()
    {
        stat.healthPoint = stat.maxHealthPoint;
        ChangeHealthBar();
    }

    //Урон
    public void Damage(int _attackPoint)
    {
        stat.healthPoint = stat.healthPoint - _attackPoint;
        ChangeHealthBar();
        DamageEffect();
    }

    //Эффект получения урона
    public void DamageEffect()
    {
        if (_hitDamageEffect != null)
        {
            StopCoroutine(_hitDamageEffect);
        }
        // Запускаем новую корутину свечения
        _hitDamageEffect = StartCoroutine(HitDamageEffect());
        Instantiate(effectDamage, gameObject.transform.position, gameObject.transform.rotation);

    }

    //Эффект получения урона
    private IEnumerator HitDamageEffect()
    {
        float timer = 0f;
        
        while (timer < flashTime)
        {
            float normalizedTime = timer / flashTime;
            // Получаем значение из кривой для текущего момента времени
            float flashAmount = flashCurve.Evaluate(normalizedTime);

            material.SetFloat("_flashAmount", flashAmount); // Устанавливаем интенсивность свечения в шейдере

            timer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с последнего кадра
            yield return null; // Ждем до следующего кадра
        }

        material.SetFloat("_flashAmount", 0f);
        _hitDamageEffect = null;
    }
}
