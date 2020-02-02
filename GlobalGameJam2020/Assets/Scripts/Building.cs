using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float minTimeNextDamage = 10.0f, maxTimeNextDamage = 20.0f;
    public float timeToRepair = 10.0f;
    public bool enableRandomDamage = true;
    public Sprite[] buildingSprites;

    public enum DebrisPosition { North, South, West, East };
    public DebrisPosition debrisPos = DebrisPosition.South;

    private bool isDamaged;
    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        //InitBuildingDebris((int)DebrisPosition.North, false);
        //InitBuildingDebris((int)DebrisPosition.South, false);
        //InitBuildingDebris((int)DebrisPosition.West, false);
        //InitBuildingDebris((int)DebrisPosition.East, false);
        
        isDamaged = false;
        UpdateBuildingState();
        if (enableRandomDamage)
        {
            InitBuildingDebris((int)debrisPos, true);
            isDamaged = true;
            StartCoroutine(ScheduleBuildingStateChange());
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering to repair the building on click
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            // If a gameObject collides with the Raycast in MousePosition
            if ((hit.collider) && (hit.collider.gameObject.name == this.name))
            {
                isDamaged = false;
                UpdateBuildingState();
                // schedule next damage
                isDamaged = true;
                StartCoroutine(ScheduleBuildingStateChange());
            }
        }

    }

    void UpdateBuildingState()
    {
        spriteIndex = isDamaged ? 1 : 0;
        transform.GetComponent<SpriteRenderer>().sprite = buildingSprites[spriteIndex];
        transform.GetChild((int)debrisPos).transform.GetComponent<SpriteRenderer>().enabled = isDamaged;
        transform.GetChild((int)debrisPos).transform.GetComponent<BoxCollider2D>().enabled = isDamaged;
        if (isDamaged)
        {
            AudioManager.instance.PlayFxSound(Sound.SoundTypes.Poop);
        }
    }
    
    void InitBuildingDebris(int position, bool state)
    {
        transform.GetChild(position).gameObject.SetActive(state);
        transform.GetChild(position).transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(position).transform.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator ScheduleBuildingStateChange()
    {
        yield return new WaitForSeconds(Random.Range(minTimeNextDamage, maxTimeNextDamage));
        UpdateBuildingState();
    }
}
