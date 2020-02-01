using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool openHorizontal = true;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("ENTROU AQUI EM KLINGON");

            if (child.GetComponent<TrafficLightChild>().isHorizontal && openHorizontal)
            {
                child.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        //Debug.Log("ENTROU AQUI EM KLINGON "+ this.GetComponentsInChildren<TrafficLightChild>());

    }

    // Update is called once per frame
    void Update()
    {

    }
}
