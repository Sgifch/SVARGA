using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statReverse : MonoBehaviour
{
    public characterStat characterStat;
    //���� ������ ����� ����� �������
    void Start()
    {
        characterStat.healthPoint = 20;
    }

}
