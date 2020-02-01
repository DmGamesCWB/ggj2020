using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool openHorizontal = true;
    public Sprite[] trafficLights;
    
    private bool isYellow = true;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure that only one Traffic Light is Green at a time
        ToggleTrafficLight();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering the traffic light and light is not Yellow
        if (Input.GetMouseButtonDown(0) && !isYellow)
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // If a gameObject collides with the Raycast in MousePosition
            if (hit.collider.gameObject.name == this.name)
            {
                Debug.Log("Toggling hit collider for " + hit.collider.name);
                openHorizontal = openHorizontal ? false : true;

                isYellow = true;
                foreach (Transform child in transform)
                {
                    bool isHorizontal = child.GetComponent<TrafficLightChild>().isHorizontal;
                    if (isHorizontal != openHorizontal)
                    {
                        child.GetComponent<SpriteRenderer>().sprite = trafficLights[2];
                    }
                }
                    
                StartCoroutine(WaitForYellowLight());
            }
        }
    }

    IEnumerator WaitForYellowLight()
    {
        yield return new WaitForSeconds(1);
        ToggleTrafficLight();
    }

    private void ToggleTrafficLight()
    {
        isYellow = false;
        foreach (Transform child in transform)
        {
            bool isHorizontal = child.GetComponent<TrafficLightChild>().isHorizontal;
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " - isHorizontal: " + isHorizontal + ", openHorizontal " + openHorizontal);
            if ((isHorizontal && openHorizontal) || (!isHorizontal && !openHorizontal))
            {
                Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " -  Disable traffic light box collider for object: " + child.name);
                child.GetComponent<BoxCollider2D>().enabled = false;
                child.GetComponent<SpriteRenderer>().sprite = trafficLights[1];
            }
            else
            {
                Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + " - Disable traffic light box collider for object: " + child.name);
                child.GetComponent<BoxCollider2D>().enabled = true;
                child.GetComponent<SpriteRenderer>().sprite = trafficLights[3];
            }
        }
    }
}
