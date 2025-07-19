using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllHealthPoint : MonoBehaviour
{
    public EnemyScriptableObject enemyProfile;

    public float currentHealthPoint;
    public GameObject effectDeath;
    public Vector3 shiftSpawnEffect;
    public float flashTime = 0.5f;
    public AnimationCurve flashCurve;
    public GameObject effectDamage;

    public bool useGameManager = false;
    private GameObject gameManager;

    private Material material;
    private Coroutine _hitDamageEffect;
    private void Awake()
    {
        currentHealthPoint = enemyProfile.healthPoint;
        if (gameObject.transform.GetChild(0) != null)
        {
            material = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        }
        else
        {
            material = gameObject.GetComponent<SpriteRenderer>().material;
        }

        if (useGameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
        }
    }
    public void Damage(int _attackPoint)
    {
        currentHealthPoint -= _attackPoint;
        if(currentHealthPoint <= 0)
        {
            Dead();
        }
        
        if (_hitDamageEffect != null)
        {
            StopCoroutine(_hitDamageEffect);
        }

        _hitDamageEffect = StartCoroutine(HitDamageEffect());
        Instantiate(effectDamage, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void Dead()
    {
        Vector3 spawnPosition = gameObject.transform.position + shiftSpawnEffect;
        Instantiate(effectDeath, spawnPosition, gameObject.transform.rotation);
        if (useGameManager)
        {
            gameManager.GetComponent<GameManager>().ActivateTrigger();
        }
        Destroy(gameObject);
    }

    private IEnumerator HitDamageEffect()
    {
        float timer = 0f;

        while (timer < flashTime)
        {
            float normalizedTime = timer / flashTime;
            // �������� �������� �� ������ ��� �������� ������� �������
            float flashAmount = flashCurve.Evaluate(normalizedTime);

            material.SetFloat("_flashAmount", flashAmount); // ������������� ������������� �������� � �������

            timer += Time.deltaTime; // ����������� ������ �� �����, ��������� � ���������� �����
            yield return null; // ���� �� ���������� �����
        }

        material.SetFloat("_flashAmount", 0f);
        _hitDamageEffect = null;
    }
}
