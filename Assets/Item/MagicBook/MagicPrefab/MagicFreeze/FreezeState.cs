using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FreezeState : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;


    private Coroutine _DurationFreeze;
    public float duration = 3f;
    public void ActiveFreeze()
    {
        if (_DurationFreeze != null)
        {
            StopCoroutine(_DurationFreeze);
        }

        _DurationFreeze = StartCoroutine(DurationFreeze());
    }

    //Замарозка
    private IEnumerator DurationFreeze()
    {
        //gameObject.GetComponent<IEnemyAI>().DisableAI();
        yield return new WaitForSeconds(duration);

        //gameObject.GetComponent<IEnemyAI>().EnableAI();
        print("Unfreeze");

        _DurationFreeze = null;
    }
}
