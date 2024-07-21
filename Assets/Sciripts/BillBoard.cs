using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    private Transform camera;
    [SerializeField] private Vector3 rotOffset = Vector3.zero;
    [SerializeField] private Vector3 posOffset = Vector3.zero;

    private void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = transform.parent.position + posOffset;
        transform.LookAt(camera);
        transform.Rotate(rotOffset, Space.Self);
    }
}
