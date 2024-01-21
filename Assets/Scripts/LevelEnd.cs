using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelNameToLoad;
    public GameObject madHatter;
    public int enemyReq;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if mad hatter != null, set active once kill count is at 20.
        if (madHatter != null && player.enemiesKilled == enemyReq - 1)
        {
            madHatter.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("collide");
            SceneManager.LoadScene(levelNameToLoad);
        }
    }
}
