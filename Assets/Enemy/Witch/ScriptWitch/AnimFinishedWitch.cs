using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFinishedWitch : MonoBehaviour
{
    public void AnimFinished()
    {
        print("AnimFinished");
        GameObject witch = gameObject.transform.parent.gameObject;
        witch.GetComponent<WitchAI>().state = WitchAI.State.Idle;
    }
}
