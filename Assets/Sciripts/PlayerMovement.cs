using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public int maxPoints = 50;
    public float maxdistpoints = 0.1f;
    public GameObject prefab;
    public string p_horizontal;
    public string p_vertical;
    private Vector3 lastPosition;
    private LineRenderer lineRenderer;
    private Queue<Vector3> positions = new Queue<Vector3>();

    void Start()
    {
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
        float moveHorizontal = Input.GetAxis(p_horizontal);
        float moveVertical = Input.GetAxis(p_vertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
        if (movement != Vector3.zero)
        {
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
        if (Input.GetKey(KeyCode.P))
        {
            foreach (Vector3 pos in positions)
            {

                Instantiate(prefab, pos, Quaternion.identity);

            }
            positions.Clear();
            lineRenderer.positionCount = 0;
        }

    }
}
