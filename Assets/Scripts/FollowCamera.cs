using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;
    float fixedYValue = 2.5f;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x + 4.5f;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        pos.y = fixedYValue;
        transform.position = pos;
    }
}
