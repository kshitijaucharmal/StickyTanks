using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridBoxMover : MonoBehaviour
{
    public Transform gridUI;
    public string slot_button;
    public float timeBtwnSwitch = 1.0f; // Time in seconds between movements
    public float waitTimeAfterSelect = 0.1f;
    public PowerupManager powerupManager; // Reference to PowerupManager

    public Transform applyEffectCanvas;
    public GameObject applyEffectPrefab;

    private int currentIndex = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;
    private string lastSelected = "";
    private List<GridItem> gridPoints = new List<GridItem>(); // Array of grid points to move between

    private bool canUsePoweup = true;

    void Start()
    {
        foreach(Transform t in gridUI)
        {
            gridPoints.Add(t.GetComponent<GridItem>());
        }
        // Set the box's initial position to the first grid point
        if (gridPoints.Count > 0)
        {
            transform.position = gridPoints[0].transform.position;
            moveCoroutine = StartCoroutine(MoveBox());
        }

    }

    void Update()
    {
        if (Input.GetButtonDown(slot_button) && canUsePoweup) {
            StartCoroutine(Use());
        }
    }

    IEnumerator Use()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        var idx = currentIndex;
        if (idx == 0) idx = 6;
        else idx -= 1;

        GridItem currentItem = gridPoints[idx];

        // Apply effect instantiate
        RawImage img = Instantiate(applyEffectPrefab, applyEffectCanvas).GetComponent<RawImage>();
        img.texture = currentItem.GetComponent<RawImage>().texture;

        PowerupType currentPowerup = currentItem.type;

        powerupManager.SetPowerupType(currentPowerup); // Notify PowerupManager

        canUsePoweup = false;
        yield return new WaitForSeconds(waitTimeAfterSelect);
        canUsePoweup = true;
        // Restart the movement coroutine
        moveCoroutine = StartCoroutine(MoveBox());
    }

    IEnumerator MoveBox()
    {
        while (isMoving)
        {
            // Snap to the current grid point
            transform.position = gridPoints[currentIndex].transform.position;

            // Move to the next grid point
            currentIndex = (currentIndex + 1) % gridPoints.Count; // Loop back to the start

            // Wait for the specified speed duration before moving again
            yield return new WaitForSeconds(timeBtwnSwitch);
        }
    }
}
