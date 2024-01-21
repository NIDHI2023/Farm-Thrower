using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBye : MonoBehaviour
{
    public float lifeSpan; //3 good

    // Update is called once per frame
    void Update()
    {
        //want to keep counting down the lifespan of the animal once it is instantiated
        lifeSpan -= Time.deltaTime;

        //will destroy
        if (lifeSpan <= 0)
        {
            DestroyAtEnd();
        }
    }


    void DestroyAtEnd()
    {
        Destroy(gameObject);
    }

    //when we collide w Farmer then destroy him and ourselves
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Farmer")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
