using UnityEngine;
using System.Collections;

public class GridBoxMover : MonoBehaviour
{
    public Transform[] gridPoints; // Array of grid points to move between
    public string slot_button;
    public float timeBtwnSwitch = 1.0f; // Time in seconds between movements
    public PowerupManager powerupManager; // Reference to PowerupManager

    private int currentIndex = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;
    private string lastSelected = "";

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
        if (Input.GetButtonDown(slot_button))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Log the current grid point name 
            // Shoudn't use name
            string currentName = gridPoints[currentIndex].name;

            PowerupType type = PowerupType.NONE;
            switch (currentName)
            {
                case "Fall":
                    type = PowerupType.SQUARE; break;
                case "Bomb":
                    type = PowerupType.CIRCLE; break;
                case "Wall":
                    type = PowerupType.TRIANGLE; break;
                default: type = PowerupType.NONE; break;
            }

            powerupManager.SetPowerupType(type); // Notify PowerupManager

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
            yield return new WaitForSeconds(timeBtwnSwitch);
        }
    }
}
