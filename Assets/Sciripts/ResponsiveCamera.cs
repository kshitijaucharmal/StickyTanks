using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [SerializeField] private float zoomLimit = -4;

    private void Start()
    {

    }

    private void Update()
    {
        if (player1 == null || player2 == null)
        {
            // One of the players is destroyed, stop further execution
            return;
        }

        var dist = Vector3.Distance(player1.position, player2.position);
        var zoomVal = Mathf.Clamp(-dist, float.NegativeInfinity, zoomLimit);

        var middle = (player1.position + player2.position) / 2;
        transform.LookAt(middle);

        var pos = transform.localPosition;
        pos.z = zoomVal;
        pos.x = middle.x;
        pos.y = dist / 5;

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 0.08f);
    }
}
