using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // 타겟
    public float offsetX = 5f;
    public float fixedYValue = 0.5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;
        Vector3 newPosition = new Vector3(targetPosition.x + offsetX, fixedYValue, transform.position.z);
        transform.position = newPosition;
    }
}
