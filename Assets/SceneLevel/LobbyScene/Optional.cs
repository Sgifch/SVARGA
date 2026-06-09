using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Optional : MonoBehaviour
{

    void Start()
    {
        PlayerPrefs.DeleteKey("counterRoom");

        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllHealthPoint>().FullRecovery();
        GameObject.FindGameObjectWithTag("Player").GetComponent<ControllManaPoint>().FullRecoveryMana();
        inventoryManager inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<inventoryManager>();
        if (File.Exists(Application.persistentDataPath + "/" + inventory._fileNameArmor))
        {
            File.Delete(Application.persistentDataPath + "/" + inventory._fileNameArmor);
        }

        PlayerPrefs.SetInt("isLobby", 1);
    }

}
