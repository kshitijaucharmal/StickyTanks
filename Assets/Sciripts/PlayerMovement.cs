using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int maxPoints = 50;
    [SerializeField] private float maxdistpoints = 0.1f;
    [SerializeField] private string p_horizontal;
    [SerializeField] private string p_vertical;

    private Vector3 lastPosition;
    private LineRenderer lineRenderer;

    [HideInInspector] public Queue<Vector3> positions = new Queue<Vector3>();
    [HideInInspector] public bool canUsePowerup = true;
   // Health health;

    void Start()
    {
       // health = new Health(100);
        lastPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.black;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw(p_horizontal);
        float moveVertical = Input.GetAxisRaw(p_vertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
        if (movement == Vector3.zero)
        {
            canUsePowerup = true;
            return;
        }
        canUsePowerup = false;


        if (Vector3.Distance(transform.position, lastPosition) > maxdistpoints)
        {
            lastPosition = transform.position;
            positions.Enqueue(transform.position);
            if (positions.Count > maxPoints)
            {
                positions.Dequeue();
            }
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }

    }

    public void ResetPositions()
    {
        positions.Clear();
        lineRenderer.positionCount = 0;
    }
}
