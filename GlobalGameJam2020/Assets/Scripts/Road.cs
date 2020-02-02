using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public float minTimeNextDamage = 10.0f, maxTimeNextDamage = 20.0f;
    public float timeToRepair = 10.0f;
    public bool enableRandomDamage = true;

    private float elapsedHealingTime;
    private bool isRoadDamaged;
    private bool isRoadBlocked;
    private bool toRepair = false;

    // Start is called before the first frame update
    void Start()
    {
        isRoadDamaged = false;
        isRoadBlocked = false;

        // Apply initial road state
        ApplyRoadState();
        if (enableRandomDamage)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            // Schedule first state change
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
            if (((hit.collider) && (hit.collider.transform.GetInstanceID() == transform.GetChild(0).GetInstanceID())))
            {
                // Start road repair
                SetNextRoadState();
                // And schedule a time for the road repair to be finished
                StartCoroutine(ApplyNextRoadStateWithinRandomTime());
            }
        }

        // Check if road is block and increase elapsed healing
        if (isRoadBlocked)
        {
            elapsedHealingTime += Time.deltaTime;
            if(elapsedHealingTime > timeToRepair)
            {
                elapsedHealingTime = 0;
                SetNextRoadState();
                ApplyNextRoadStateWithinRandomTime();
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
            toRepair = true;
            Debug.Log("REPAIR TRUE");
        }
        ApplyRoadState();
    }

    void ApplyRoadState()
    {
        // Either road damage or road block should be 
        //Debug.Log(gameObject.name + " Applying state");
        GameObject hole = transform.GetChild(0).gameObject;
        hole.transform.GetComponent<SpriteRenderer>().enabled = isRoadDamaged;
        hole.GetComponent<BoxCollider2D>().enabled = isRoadDamaged;

        GameObject roadBlock = transform.GetChild(1).gameObject;
        roadBlock.gameObject.transform.GetComponent<SpriteRenderer>().enabled = isRoadBlocked;
        roadBlock.gameObject.GetComponent<BoxCollider2D>().enabled = isRoadBlocked;

    }

    IEnumerator ApplyNextRoadStateWithinRandomTime()
    {
        //Debug.Log(gameObject.name + " - Next state change will take " + randomTime);
        if (toRepair)
        {
            Debug.Log("REPAIR TRUE PLAY");
            AudioManager.instance.PlayFxSound(Sound.SoundTypes.RepairingLong);
            toRepair = false;
        }
        yield return new WaitForSeconds(Random.Range(minTimeNextDamage, maxTimeNextDamage));
        SetNextRoadState();
    }
}
