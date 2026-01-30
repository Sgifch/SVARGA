using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFinishedBuivol : MonoBehaviour
{
    public GameObject buivol;
    public void AttackFinished()
    {
        buivol.GetComponent<AIBuivol>().state = AIBuivol.State.Idle;
    }
}
