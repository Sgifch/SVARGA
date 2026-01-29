using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AnimFinishedKikimora : MonoBehaviour
{
    public GameObject kikimora;
    public void AttackFinished()
    {
        kikimora.GetComponent<AIKikimora>().state = AIKikimora.State.Roaming;
    }
}
