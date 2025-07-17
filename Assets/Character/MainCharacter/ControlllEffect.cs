using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlllEffect : MonoBehaviour
{
    private Material mainCharacterMaterial;

    private void Awake()
    {
        mainCharacterMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void DamageEffect()
    {

    }
}
