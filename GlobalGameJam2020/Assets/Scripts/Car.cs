using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Physics Variables
    public float maxSpeed = 15.0f;
    public float acceleration = 0.5f;
    
    public bool isHorizontal = true;

    private float speed;

    public float score = 1.0f;
    public float secondsStoppedToDowngrade = 5.0f;
    public float secondsMovingToUpgrade = 10.0f;
    public float scoreStepSize = .2f;
    public float timeStopped = 0.0f;
    public float timeMoving = 0.0f;

    public float bottonOffset = 50f;
    public float rightOffset = 120f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

// Update is called once per frame
    void Update()
    {
        float prevSpeed = speed;
        speed += acceleration;
        speed = Mathf.Min(speed , maxSpeed);
        speed = Mathf.Max(speed, 0);

        // Check Stopped/Moving time
        if(prevSpeed == 0 && speed == 0)
        {
            timeStopped += Time.deltaTime;
            timeMoving = 0;
        }
        if (prevSpeed > 0 && speed > 0)
        {
            timeMoving += Time.deltaTime;
            timeStopped = 0;
        }
        
        // Check If stopped/moved for time enough to change score
        if(timeStopped > secondsStoppedToDowngrade)
        {
            score = Mathf.Max(0, score - scoreStepSize);
            timeStopped = 0;
        }
        if(timeMoving > secondsMovingToUpgrade)
        {
            score = Mathf.Min(score + scoreStepSize, 1.0f);
            timeMoving = 0;
        }
        
        // Actually moves the car
        if (isHorizontal)
        {
            this.transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else
        {
            this.transform.position += new Vector3(0, -speed*Time.deltaTime, 0);
        }
        destroyOutOfBounds();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("trafficLight"))
        {
            if (acceleration > 0)
            {
                acceleration = -acceleration;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (acceleration < 0)
        {
            acceleration = -acceleration;
        }
    }

    private void destroyOutOfBounds()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 carPos = transform.position;

        if (carPos.y < -bottonOffset || carPos.x > rightOffset)
        {
            Debug.Log("POSX:" + carPos.x + " POSY:" + carPos.y);
            Destroy(this.gameObject);
        }

    }
}
