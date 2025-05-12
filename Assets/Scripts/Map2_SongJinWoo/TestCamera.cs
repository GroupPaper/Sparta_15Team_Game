using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestCamera : MonoBehaviour
{
    public Transform targetTransform;
    public float cameraSmoothSpeed = 0.125f;
    public float cameraOffsetX = 0f;

    private Vector3 cameraVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (targetTransform == null) return;

        Vector3 targetPosition = new Vector3(targetTransform.position.x + cameraOffsetX, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, cameraSmoothSpeed);
    }
}