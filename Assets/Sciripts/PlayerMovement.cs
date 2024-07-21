using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Quaternion offsetRot = Quaternion.Euler(0, 270, 0);
    [SerializeField] private int maxPoints = 50;
    [SerializeField] private float maxdistpoints = 0.1f;
    [SerializeField] private string p_horizontal;
    [SerializeField] private string p_vertical;

    private Vector3 lastPosition;
    private LineRenderer lineRenderer;

    [HideInInspector] public Queue<Vector3> positions = new Queue<Vector3>();
    [HideInInspector] public bool canUsePowerup = true;
    [HideInInspector] public Health health;
    public HealthBar healthbar;

    void Start()
    {
        healthbar.SetMaxHealth(100);
        health = new Health(100,healthbar);
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
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        if (movement == Vector3.zero)
        {
            canUsePowerup = true;
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation * offsetRot, rotationSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, lastPosition) > maxdistpoints) {
            lastPosition = transform.position;
            positions.Enqueue(transform.position);
            if (positions.Count > maxPoints) {
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
