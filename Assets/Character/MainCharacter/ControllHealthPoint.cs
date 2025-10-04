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

    //���������� ������� ������� � bar
    public void ChangeHealthBar() 
    {
        currentHealthPoint = (float)(playerStat.currentHP) / playerStat.currentMaxHP;
        healthBar.GetComponent<Image>().fillAmount = currentHealthPoint;
    }

    //�������������
    public void Recovery(int _recoveryPoint) //����������
    {
        if(playerStat.currentHP < playerStat.maxHP)
        {
            playerStat.currentHP = playerStat.currentHP + _recoveryPoint;
        }
        ChangeHealthBar();
    }

    //������ ��������������
    public void FullRecovery()
    {
        playerStat.currentHP = playerStat.maxHP;
        ChangeHealthBar();
    }

    //����
    public void Damage(int _attackPoint)
    {
        playerStat.currentHP = playerStat.currentHP - _attackPoint;
        ChangeHealthBar();
        DamageEffect();
    }

    //������ ��������� �����
    public void DamageEffect()
    {
        if (_hitDamageEffect != null)
        {
            StopCoroutine(_hitDamageEffect);
        }
        // ��������� ����� �������� ��������
        _hitDamageEffect = StartCoroutine(HitDamageEffect());
        Instantiate(effectDamage, gameObject.transform.position, gameObject.transform.rotation);

    }

    //������ ��������� �����
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
