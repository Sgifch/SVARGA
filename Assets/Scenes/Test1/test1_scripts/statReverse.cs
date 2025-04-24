using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statReverse : MonoBehaviour
{
    public characterStat characterStat;
    //Этот скрипт потом нужно удалить
    void Start()
    {
        characterStat.healthPoint = 20;
    }

}
