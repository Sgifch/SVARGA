using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Получаем корневой объект (родителя)
        GameObject rootObject = other.transform.root.gameObject;

        // Проверяем тег у корневого объекта
        if (rootObject.CompareTag("Pet"))
        {
            UnityEngine.AI.NavMeshAgent agent = rootObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }

            Destroy(rootObject);
        }
    }
}
