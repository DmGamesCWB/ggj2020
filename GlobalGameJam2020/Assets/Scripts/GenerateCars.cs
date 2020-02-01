using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCars : MonoBehaviour
{
    public GameObject trafficLight;
    public GameObject car;
    public int numberOfCars = 10;

    private int carsInstantiated = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateCar());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InstantiateCar()
    {
        yield return new WaitForSeconds(3);
        float xPosition = -50.0f;
        float yPosition = 50.0f;
        float rotation = 0f;
        if (trafficLight.gameObject.name.Contains("VTrafficLight"))
        {
            
            xPosition = trafficLight.transform.position.x;
            yPosition = 50;
            rotation = -180;
            Debug.Log("Entered VTrafficLight " + xPosition + ":" + yPosition);
        } else if (trafficLight.gameObject.name.Contains("HTrafficLight"))
        {
            xPosition = -50;
            yPosition = trafficLight.transform.position.y;
            rotation = -90;
            Debug.Log("Entered HTrafficLight " + xPosition + ":" + yPosition);
        }
     
        
        Instantiate(car, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(new Vector3(0, 0, rotation)));
        carsInstantiated++;
        if (carsInstantiated < numberOfCars)
        {
            StartCoroutine(InstantiateCar());
        }
    }
}
