using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ControllHealthPoint : MonoBehaviour
{
    //public characterStat stat;
    public PlayerStatManager playerStat;
    public float flashTime = 1f;
    public AnimationCurve flashCurve;
    public GameObject effectDamage;

    public bool useGameManager;

    private GameObject gameManager;
    private float currentHealthPoint;
    private GameObject healthBar;
    private Material material;

    private Coroutine _hitDamageEffect;
    private Coroutine _DurationRecoveryFunction;
    private Coroutine _DurationDamageFunction;
    private enum State
    {
        Idle,
        ContinuosDamage
    }

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar");
        material = GetComponent<SpriteRenderer>().material;
        playerStat = GameObject.Find("PlayerStatManager").GetComponent<PlayerStatManager>();
    }
    private void Start()
    {
        //ChangeHealthBar();
        Invoke("ChangeHealthBar", 0.3f);
    }

    //Изменеение размера полоски в bar
    public void ChangeHealthBar() 
    {
        currentHealthPoint = (float)(playerStat.currentHP) / playerStat.currentMaxHP;
        healthBar.GetComponent<Image>().fillAmount = currentHealthPoint;
    }

    //Востановление---------------------------------------------------------------------------------------------------------------
    public void Recovery(int _recoveryPoint) //переделать
    {
        if(playerStat.currentHP < playerStat.currentMaxHP)
        {
            if((playerStat.currentMaxHP - playerStat.currentHP) <= _recoveryPoint)
            {
                playerStat.currentHP = playerStat.currentHP + _recoveryPoint;
            }
            else
            {
                playerStat.currentHP = playerStat.currentMaxHP;
            }
        }
        ChangeHealthBar();
    }

    //Полное восстановление
    public void FullRecovery()
    {
        playerStat.currentHP = playerStat.maxHP;
        ChangeHealthBar();
    }

    //Продолжительные восстановление
    public void DurationRecovery(int _recoveryPoint, float _time, float _seconds)
    {
        if (_DurationRecoveryFunction != null)
        {
            StopCoroutine(_DurationRecoveryFunction);
        }

        _DurationRecoveryFunction = StartCoroutine(DurationRecoveryFunction(_recoveryPoint, _time, _seconds));
    }

    //Урон--------------------------------------------------------------------------------------------------------------------------
    public void Damage(int _attackPoint)
    {
        //Мб сделать проверку на отрицательно или равное 0 хп
        playerStat.currentHP = playerStat.currentHP - _attackPoint;
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

    //Продолжительный урон
    public void DurationDamage(int _attackPoint, float _time, float _seconds)
    {
        if (_DurationDamageFunction != null)
        {
            StopCoroutine(_DurationDamageFunction);
        }

        _DurationDamageFunction = StartCoroutine(DurationDamageFunction(_attackPoint, _time, _seconds));
    }

    //Корутины-----------------------------------------------------------------------------------------------------------------------
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

    //Продолжительное_восстановление
    private IEnumerator DurationRecoveryFunction(int _recoveryPoint, float _time, float _interval)
    {
        float timer = 0f;
        float timerInv = 0f;
        while(timer < _time)
        {
            if (timerInv > _interval)
            {
                Recovery(_recoveryPoint);
                timerInv = 0f;
            }
            timer += Time.deltaTime;
            timerInv += Time.deltaTime;
            yield return null;
        }

        _DurationRecoveryFunction = null;
    }

    //Продолжительный_урон
    private IEnumerator DurationDamageFunction(int _attackPoint, float _time, float _interval)
    {
        float timer = 0f;
        float timerInv = 0f;
        while (timer < _time)
        {
            if (timerInv > _interval)
            {
                Damage(_attackPoint);
                timerInv = 0f;
            }
            timer += Time.deltaTime;
            timerInv += Time.deltaTime;
            yield return null;
        }

        _DurationDamageFunction = null;
    }
}
