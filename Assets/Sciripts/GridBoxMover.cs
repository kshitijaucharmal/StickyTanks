using UnityEngine;
using System.Collections;

public class GridBoxMover : MonoBehaviour
{
    public Transform[] gridPoints; // Array of grid points to move between
    public KeyCode slot_button;
    public float speed = 1.0f; // Time in seconds between movements
    private int currentIndex = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;
    private string lastSelected = "";
    private string secondLastSelected = "";
    public PowerupManager powerupManager; // Reference to PowerupManager

    void Start()
    {
        // Set the box's initial position to the first grid point
        if (gridPoints.Length > 0)
        {
            transform.position = gridPoints[0].position;
            moveCoroutine = StartCoroutine(MoveBox());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(slot_button))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Log the current grid point name
            string currentName = gridPoints[currentIndex].name;
            Debug.Log("Selected shape: " + currentName);

            // Check for consecutive "Bomb" or "Wall" or "Fall" selections
            secondLastSelected = lastSelected;
            lastSelected = currentName;

            if (lastSelected == secondLastSelected && (lastSelected == "Bomb" || lastSelected == "Wall" || lastSelected == "Fall"))
            {
                Debug.Log(lastSelected + " selected twice consecutively");
                powerupManager.SetPowerupType(lastSelected); // Notify PowerupManager
            }

            // Restart the movement coroutine
            moveCoroutine = StartCoroutine(MoveBox());
        }
    }

    IEnumerator MoveBox()
    {
        while (isMoving)
        {
            // Snap to the current grid point
            transform.position = gridPoints[currentIndex].position;

            // Move to the next grid point
            currentIndex = (currentIndex + 1) % gridPoints.Length; // Loop back to the start

            // Wait for the specified speed duration before moving again
            yield return new WaitForSeconds(speed);
        }
    }
}
