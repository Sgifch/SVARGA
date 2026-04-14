using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFinishBoar : MonoBehaviour
{
    public GameObject boar;
    public void AttackFinished()
    {
        boar.GetComponent<BoarAI>().state = BoarAI.State.Idle;
    }
}
