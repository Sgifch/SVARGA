using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statReverse : MonoBehaviour
{
    public characterStat characterStat;
    //Этот скрипт потом нужно удалить
    void Awake()
    {
        characterStat.healthPoint = 20;
    }

}
