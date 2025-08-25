using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public PlayerStatManager playerStat;
    public string sceneName;

    public void Awake()
    {
        playerStat = GameObject.FindWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStat.SaveStat();
            SceneManager.LoadScene(sceneName);
        }
    }
}
