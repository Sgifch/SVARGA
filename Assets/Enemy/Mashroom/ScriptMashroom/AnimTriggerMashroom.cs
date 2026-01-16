using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerMashroom : MonoBehaviour
{
    public void SetParent()
    {
        gameObject.transform.parent.gameObject.GetComponent<AIMashroom>().Attack();
    }
}
