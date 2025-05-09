using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    private GroundChecker _groundChecker;
    private JumpController _jumpController;

    private bool isSliding;

    public void Init(JumpController jumpController)
    {
        _jumpController = jumpController;
    }

    public void TrySlide()
    {
        if (_jumpController.JumpCount == 0)
        {
            isSliding = true;
        }
    }

    public void Sliding()
    {
        if (isSliding)
        {
            Debug.Log("슬라이딩");
            // 애니메이션
            // 슬라이드 액티브
        }
    }

    public void EndSlide()
    {
        isSliding = false;
        // 애니메이션
        // 런 액티브
    }

    public bool IsSliding()
    {
        return isSliding;
    }
}
