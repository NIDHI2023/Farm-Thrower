using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hitsReq;
    private PlayerController player;
     
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitsReq == 0)
        {
            player.enemiesKilled++;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        hitsReq--;
    }
}
