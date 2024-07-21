using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridBoxMover : MonoBehaviour
{
    public Transform gridUI;
    public string slot_button;
    public float timeBtwnSwitch = 1.0f; // Time in seconds between movements
    public PowerupManager powerupManager; // Reference to PowerupManager

    private int currentIndex = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;
    private string lastSelected = "";
    private List<GridItem> gridPoints = new List<GridItem>(); // Array of grid points to move between

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
        if (Input.GetButtonDown(slot_button)) {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Log the current grid point name 
            // Shoudn't use name
            PowerupType currentPowerup = gridPoints[currentIndex].type;

            powerupManager.SetPowerupType(currentPowerup); // Notify PowerupManager

            // Restart the movement coroutine
            moveCoroutine = StartCoroutine(MoveBox());
        }
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
