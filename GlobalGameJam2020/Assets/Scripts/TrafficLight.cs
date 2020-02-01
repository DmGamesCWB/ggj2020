using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool openHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        ToggleTrafficLight();
        //Debug.Log("ENTROU AQUI EM KLINGON "+ this.GetComponentsInChildren<TrafficLightChild>());

    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering the traffic light
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // If a gameObject collides with the Raycast in MousePosition
            if (hit.collider != null)
            {
                Debug.Log("Toggling hit collider for " + hit.collider.name);
                openHorizontal = openHorizontal ? false : true;
                ToggleTrafficLight();
            }
        }
    }

    private void ToggleTrafficLight()
    {
        foreach (Transform child in transform)
        {
            bool isHorizontal = child.GetComponent<TrafficLightChild>().isHorizontal;
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " - isHorizontal: " + isHorizontal + ", openHorizontal " + openHorizontal);
            if ((isHorizontal && openHorizontal) || (!isHorizontal && !openHorizontal))
            {
                Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " -  Disable traffic light box collider for object: " + child.name);
                child.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " - Disable traffic light box collider for object: " + child.name);
                child.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
