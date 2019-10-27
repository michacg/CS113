using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    private float zoom = 10f;

    private void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
    }
    void LateUpdate()
    {
        transform.position = target.position - offset * zoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
