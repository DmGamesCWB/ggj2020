using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Physics Variables
    public float maxSpeed = 15.0f;
    public float acceleration = 0.5f;
    
    public bool isHorizontal = true;

    public bool isBroken = false;

    private float speed;

    public float score = 1.0f;
    public float secondsStoppedToDowngrade = 5.0f;
    public float secondsMovingToUpgrade = 10.0f;
    private int driverEmojiIndex = 0;
    public float timeStopped = 0.0f;
    public float timeMoving = 0.0f;
    public Sprite[] driverEmojis;

    public float bottonOffset = 50f;
    public float rightOffset = 120f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // If car is broken, it can not accelerate
        if (isBroken)
        {
            if (acceleration > 0)
            {
                acceleration = -acceleration;
            }

            // Check if it can be fixed
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                // If a gameObject collides with the Raycast in MousePosition
                if (hit.collider.transform.GetInstanceID() == transform.GetChild(2).GetInstanceID())
                {
                    FixCar();
                }
            }
        }

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
            // timeStopped does not come to zero right after you start moving...
            // What? Do you think it is that easy to recover from a bad humor??
            timeStopped = Mathf.Max(0, timeStopped - Time.deltaTime);
        }
        
        // Check If stopped/moved for time enough to change score
        if(timeStopped > secondsStoppedToDowngrade)
        {
            score = Mathf.Max(0, score - (1.0f / driverEmojis.Length));
            driverEmojiIndex = Mathf.Min(driverEmojiIndex + 1, driverEmojis.Length - 1);
            timeStopped = 0;
        }
        if(timeMoving > secondsMovingToUpgrade)
        {
            score = Mathf.Min(score + (1.0f / driverEmojis.Length), 1.0f);
            driverEmojiIndex = Mathf.Max(driverEmojiIndex - 1, 0);
            timeMoving = 0;
        }

        // Update Driver Emoji
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = driverEmojis[driverEmojiIndex];

        // Actually moves the car
        if (isHorizontal)
        {
            this.transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else
        {
            this.transform.position += new Vector3(0, -speed*Time.deltaTime, 0);
        }
        DestroyOutOfBounds();
    }
    
    public void BreakCar()
    {
        isBroken = true;
        score /= 2; // Best way to ruin your day...
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void FixCar()
    {
        isBroken = false;
        if(acceleration < 0)
        {
            acceleration = -acceleration;
        }
        transform.GetChild(3).gameObject.SetActive(false);
    }

    private void DestroyOutOfBounds()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 carPos = transform.position;

        if (carPos.y < -bottonOffset || carPos.x > rightOffset)
        {
            Destroy(this.gameObject);
        }

    }
}
