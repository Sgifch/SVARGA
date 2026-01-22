using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFinishedWitch : MonoBehaviour
{
    public GameObject spell;
    private GameObject currentSpell;
    public GameObject spawn;
    public float speed;
    public void AnimFinished()
    {
        print("AnimFinished");
        GameObject witch = gameObject.transform.parent.gameObject;
        currentSpell = Instantiate(spell, spawn.transform.position, transform.rotation);
        currentSpell.GetComponent<Rigidbody2D>().velocity = new Vector2(witch.GetComponent<WitchAI>().lookDirection.x, witch.GetComponent<WitchAI>().lookDirection.y)*speed;
        witch.GetComponent<WitchAI>().state = WitchAI.State.Idle;
    }
}
