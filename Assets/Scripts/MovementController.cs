using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private float forwardSpeed; // 기본 속도
    private float acceleration; // 초당 가속력
    private float maxSpeed; // 현재 최고 속도
    private float originalMaxSpeed; // 최고 속도 기본값 (아이템 버프 지속효과 끝나고 복구하는 용도)
    private float totalAcceleration; // 누적 가속도

    private MonoBehaviour runner; // 코루틴이 MonoBehaviour를 상속해야 사용할 수 있는데 MovementController는 그게 아니여서 임시로 실행하도록 조치 상속하면 Player.cs에서 객체 생성 오류뜸;

    public void Init(float forwardSpeed, float acceleration, float maxSpeed)
    {
        this.forwardSpeed = forwardSpeed;
        this.acceleration = acceleration;
        this.maxSpeed = maxSpeed;
        this.originalMaxSpeed = maxSpeed; // 버프 복구용 기본값
        totalAcceleration = 0f;
    }

    public void ApplySpeedItemBuff(float bonusSpeed, float duration)
    {
        maxSpeed = originalMaxSpeed + bonusSpeed;
        if(runner != null)
        {
            runner.StartCoroutine(ResetMaxSpeedAfter(duration));
        }
    }

    private IEnumerator ResetMaxSpeedAfter(float duration) // duration 시간 만큼 버프 지속효과 제공 함수
    {
        yield return new WaitForSeconds(duration);
        maxSpeed = originalMaxSpeed; // 버프 이전 속도로 초기화
    }

    public float GetCurrentSpeed()
    {
        totalAcceleration += acceleration * Time.deltaTime;
        totalAcceleration = Mathf.Min(totalAcceleration, maxSpeed - forwardSpeed);

        float currentSpeed = forwardSpeed + totalAcceleration;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        // Debug.Log($"현재속도: {currentSpeed}, 누적가속도: {totalAcceleration}");

        return currentSpeed;
    }

    public float GetTotalAcceleration()
    {
        return totalAcceleration;
    }
}


