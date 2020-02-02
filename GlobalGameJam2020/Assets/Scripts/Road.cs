using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public int startSpriteIndex = 0;
    public float minTime = 1.0f, maxTime = 2.0f;
    public bool enableRandomDamage = true;

    private float randomTime;
    private bool isRoadDamaged;
    private bool isRoadBlocked;

    // Start is called before the first frame update
    void Start()
    {
        isRoadDamaged = false;
        isRoadBlocked = false;
        SetRandomTime();
        // Apply initial road state
        ApplyNextRoadState();
        if (enableRandomDamage)
        {
            // Schedule first state change
            SetNextRoadState();
            StartCoroutine(ApplyNextRoadStateWithinRandomTime());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering a non-broken road with random road blocks enabled
        if (enableRandomDamage && isRoadDamaged && Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            //Debug.Log(hit.collider.transform.name + " / " + transform.GetChild(0).name + " - Road got clicked");
            //Debug.Log(hit.collider.transform.GetInstanceID() + " / " + transform.GetChild(0).GetInstanceID() + " - Road got clicked");
            // If a gameObject collides with the Raycast in MousePosition
            if ((hit.collider.transform.GetInstanceID() == transform.GetChild(0).GetInstanceID()))                
            {
                Debug.Log(hit.collider.gameObject.name + " / " + gameObject.name + " - Road got clicked");
                // Start road repair
                SetNextRoadState();
                ApplyNextRoadState();
                // And schedule a time for the road repair to be finished
                SetNextRoadState();
                StartCoroutine(ApplyNextRoadStateWithinRandomTime());
            }
        }
    }

    void SetNextRoadState()
    {
        if (!isRoadDamaged && !isRoadBlocked)
        {
            //Debug.Log(gameObject.name + " - Next state is damaged");
            isRoadDamaged = true;
            isRoadBlocked = false;
        }
        else if (isRoadDamaged && !isRoadBlocked)
        {
            //Debug.Log(gameObject.name + " - Next state is blocked");
            isRoadDamaged = false;
            isRoadBlocked = true;
        }
        else
        {
            //Debug.Log(gameObject.name + " - Next state is good");
            isRoadDamaged = false;
            isRoadBlocked = false;
        }
    }

    void ApplyNextRoadState()
    {
        // Either road damage or road block should be 
        //Debug.Log(gameObject.name + " Applying state");
        transform.GetChild(0).gameObject.SetActive(isRoadDamaged);
        transform.GetChild(0).gameObject.transform.GetComponent<SpriteRenderer>().enabled = isRoadDamaged;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = isRoadDamaged;

        transform.GetChild(1).gameObject.SetActive(isRoadBlocked);
        transform.GetChild(1).gameObject.transform.GetComponent<SpriteRenderer>().enabled = isRoadBlocked;
        transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = isRoadBlocked;
    }


    IEnumerator ApplyNextRoadStateWithinRandomTime()
    {
        //Debug.Log(gameObject.name + " - Next state change will take " + randomTime);
        yield return new WaitForSeconds(randomTime);
        this.ApplyNextRoadState();
    }

    private void SetRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
    }
}
