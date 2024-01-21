using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalThrower : MonoBehaviour
{
    private GameObject animalPrefab;
    private Camera theCam;
    public float animalImpulse; //25-100f
    private GameObject theAnimal;

    public List<GameObject> animalObjects;

    private int index; //get random num for index in the size of our List

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowAnimal();
    }

    void ThrowAnimal()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //want to randomly get an index number and then make that animal what gets thrown
            index = (int)Random.Range(0, animalObjects.Count);

            //store the animal object at the random index this is what will get Instantiitated
            animalPrefab = animalObjects[index];

            theAnimal = (GameObject) Instantiate(animalPrefab, theCam.transform.position + theCam.transform.forward, theCam.transform.rotation);
            theAnimal.GetComponent<Rigidbody>().AddForce(theCam.transform.forward * animalImpulse, ForceMode.Impulse);
        }
    }
}
