﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
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
        if (other.CompareTag("Car") || other.CompareTag("roadBlock"))
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
        //Debug.Log("Saiu da área do carro o " + other.name);
        if (other.CompareTag("Car") || other.CompareTag("roadBlock"))
        {
            float acc = GetComponentInParent<Car>().acceleration;
            if (acc < 0)
            {
                GetComponentInParent<Car>().acceleration = -acc;
            }
        }
    }
}
