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
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

// Update is called once per frame
void Update()
    {
        speed += acceleration;
        speed = Mathf.Min(speed , maxSpeed);
        speed = Mathf.Max(speed, 0);

        if (isHorizontal)
        {
            this.transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else
        {
            this.transform.position += new Vector3(0, -speed*Time.deltaTime, 0);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("trafficLight"))
    //    {
    //        speed = 0f;
    //    }
    //}

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
}
