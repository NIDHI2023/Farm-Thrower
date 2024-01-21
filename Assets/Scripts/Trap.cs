using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private Vector3 startPos;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.enemiesKilled = 0;
            if (gameObject.tag == "Trap")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else
            {
                SceneManager.LoadScene("level1");
            }
        }
    }
}
