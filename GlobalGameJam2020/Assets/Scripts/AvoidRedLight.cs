using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidRedLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("trafficLight"))
        {
            float acc = GetComponentInParent<Car>().acceleration;
            if (acc > 0)
            {
                GetComponentInParent<Car>().acceleration = -acc;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("trafficLight"))
        {
            float acc = GetComponentInParent<Car>().acceleration;
            if (acc < 0)
            {
                GetComponentInParent<Car>().acceleration = -acc;
            }
        }
    }
}
