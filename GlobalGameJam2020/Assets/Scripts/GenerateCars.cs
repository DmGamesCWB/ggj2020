using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCars : MonoBehaviour
{
    public GameObject trafficLightForPositionReference;
    public GameObject car;
    public int numberOfCars = 10;
    public int intervalGenerationSeconds = 3;
    public float maxJitterGenerationSeconds = 1.0f;

    private int carsInstantiated = 0;
    private bool isHorizontal;
    private bool isSummonPlaceHolderBusy;

    // Start is called before the first frame update
    void Start()
    {
        // Check if road where car needs to be instantiated is horizontal
        isHorizontal = trafficLightForPositionReference.GetComponent<TrafficLightChild>().isHorizontal;
        Debug.Log(trafficLightForPositionReference.name + " é horizontal:" + isHorizontal);
        if (numberOfCars > 0)
        {
            StartCoroutine(JitterGenerator());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator JitterGenerator()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, maxJitterGenerationSeconds));

        // Check if summon placeholder is free for new instance of car
        if (isSummonPlaceHolderBusy)
        {
            StartCoroutine(JitterGenerator());
        }
        else
        {
            StartCoroutine(InstantiateCar());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            isSummonPlaceHolderBusy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            isSummonPlaceHolderBusy = false;
        }
    }

    IEnumerator InstantiateCar()
    {
        yield return new WaitForSeconds(intervalGenerationSeconds);
        float xPosition = -50.0f;
        float yPosition = 50.0f;
        float rotation = 0f;

        if (!isHorizontal)
        {   
            xPosition = trafficLightForPositionReference.transform.position.x;
            yPosition = GetComponent<BoxCollider2D>().transform.position.y;
            rotation = -180;
            Debug.Log("Entered VTrafficLight " + xPosition + ":" + yPosition);
        }
        else
        {
            xPosition = GetComponent<BoxCollider2D>().transform.position.x;
            yPosition = trafficLightForPositionReference.transform.position.y;
            rotation = -90;
            Debug.Log("Entered HTrafficLight " + xPosition + ":" + yPosition);
        }
        
        Instantiate(car, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(new Vector3(0, 0, rotation)));
        carsInstantiated++;
        if (carsInstantiated < numberOfCars)
        {
            StartCoroutine(JitterGenerator());
        }
    }
}
