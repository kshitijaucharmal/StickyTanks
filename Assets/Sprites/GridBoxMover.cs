using UnityEngine;
using System.Collections;

public class GridBoxMover : MonoBehaviour
{
    public Transform[] gridPoints; // Array of grid points to move between
    public float speed = 1.0f; // Time in seconds between movements
    private int currentIndex = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;

    void Start()
    {
        if (gridPoints.Length > 0)
        {
            transform.position = gridPoints[0].position;
            // Start the movement coroutine
            moveCoroutine = StartCoroutine(MoveBox());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            Debug.Log("Selected shape: " + gridPoints[currentIndex].name);
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
