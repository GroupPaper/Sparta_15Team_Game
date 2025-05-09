using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private float forwardSpeed;
    private float acceleration;
    private float maxSpeed;
    private float totalAcceleration;

    public void Init(float forwardSpeed, float acceleration, float maxSpeed)
    {
        this.forwardSpeed = forwardSpeed;
        this.acceleration = acceleration;
        this.maxSpeed = maxSpeed;
        totalAcceleration = 0f;
    }

    public Vector2 CalculateVelocity(Vector2 currentVelocity)
    {
        totalAcceleration += acceleration * Time.fixedDeltaTime;
        totalAcceleration = Mathf.Min(totalAcceleration, maxSpeed - forwardSpeed);

        float currentSpeed = forwardSpeed + totalAcceleration;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        currentVelocity.x = currentSpeed;
        return currentVelocity;
    }

    public float GetTotalAcceleration()
    {
        return totalAcceleration;
    }
}


