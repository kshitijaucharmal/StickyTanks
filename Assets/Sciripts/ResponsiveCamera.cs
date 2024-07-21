using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Transform lookAtTarget;

    [SerializeField] private float zoomLimit = -4;
    [Range(0f, 1f)][SerializeField] private float smoothing = 0.1f;

    [HideInInspector] public bool gamePaused = true;

    private Vector3 middle;

    Vector3 SetLookAtTarget()
    {
        if(gamePaused)
        {
            return lookAtTarget.position;
        }
        return (player1.position + player2.position) / 2;
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

        middle = Vector3.Lerp(middle, SetLookAtTarget(), smoothing);
        transform.LookAt(middle);

        var pos = transform.localPosition;
        pos.z = zoomVal;
        pos.x = middle.x;
        pos.y = dist / 5;

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 0.08f);
    }
}
