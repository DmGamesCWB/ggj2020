using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float initialSpeed = 15.0f;
    public bool isHorizontal = true;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = initialSpeed;
    }

// Update is called once per frame
void Update()
    {
        if (isHorizontal)
        {
            this.transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else
        {
            this.transform.position += new Vector3(0, -speed*Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter2D
    {
        if (other.CompareTag("trafficLight"))
        {
            speed = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed = initialSpeed;
    }
}
