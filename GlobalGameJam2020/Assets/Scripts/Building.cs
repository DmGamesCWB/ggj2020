using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isDamaged = true;
    public Sprite[] buildingSprites;
    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBuildingState();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if MouseClick occurred while hovering
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            // If a gameObject collides with the Raycast in MousePosition
            if (hit.collider.gameObject.name == this.name)
            {
                isDamaged = false;
                UpdateBuildingState();
            }
        }
    }

    void UpdateBuildingState()
    {
        spriteIndex = isDamaged ? 1 : 0;
        transform.GetComponent<SpriteRenderer>().sprite = buildingSprites[spriteIndex];
    }
}
