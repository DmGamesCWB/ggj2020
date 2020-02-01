using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public int startSpriteIndex = 0;
    public float minTime = 1.0f, maxTime = 2.0f;
    public Sprite[] roadSprites;

    private float randomTime;
    private int currentSpriteIndex;
    private bool isRoadBlocked;

    // Start is called before the first frame update
    void Start()
    {
        currentSpriteIndex = startSpriteIndex;
        isRoadBlocked = false;
        SetRandomTime();
        // Apply initial road state
        ApplyNextRoadState();
        // Schedule first state change
        SetNextRoadState();
        StartCoroutine(ApplyNextRoadStateWithinRandomTime());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering a non-broken road
        if (currentSpriteIndex != 0 && Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name + " - Road got clicked");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // If a gameObject collides with the Raycast in MousePosition
            if (hit.collider.gameObject.CompareTag("roadBlock"))
            {
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
        // sets next sprite index and also updates isRoadBlocked flag
        currentSpriteIndex++;
        if (currentSpriteIndex >= roadSprites.Length)
        {
            currentSpriteIndex = 0;
            isRoadBlocked = false;
        }
        else
        {
            isRoadBlocked = true;
        }
        Debug.Log(gameObject.name + " - Next road state index " + currentSpriteIndex);
    }

    void ApplyNextRoadState()
    {
        this.GetComponent<SpriteRenderer>().sprite = roadSprites[currentSpriteIndex];
        this.GetComponent<BoxCollider2D>().enabled = isRoadBlocked;
    }


    IEnumerator ApplyNextRoadStateWithinRandomTime()
    {
        Debug.Log(gameObject.name + " - Next state change will take " + randomTime);
        yield return new WaitForSeconds(randomTime);
        this.ApplyNextRoadState();
    }

    private void SetRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
    }
}
