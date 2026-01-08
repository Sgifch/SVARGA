using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineScript : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject mainCharacter;
    public GameObject UI;

    private void Awake()
    {
        mainCharacter = GameObject.FindWithTag("Player");
        UI = GameObject.FindWithTag("UIControll");
    }

    private void Update()
    {
        if (director.state == PlayState.Playing)
        {
            mainCharacter.GetComponent<ControllMove>().enabled = false;
            UI.GetComponent<UIControll>().stateUI = UIControll.StateUI.cutscene;
        }
        else
        {
            mainCharacter.GetComponent<ControllMove>().enabled = true;
            UI.GetComponent<UIControll>().stateUI = UIControll.StateUI.idle;
            Destroy(gameObject);
        }
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        mainCharacter.GetComponent<ControllMove>().enabled = true;
    }

    private void Director_Played(PlayableDirector obj)
    {
        mainCharacter.GetComponent<ControllMove>().enabled = false;
    }
}
