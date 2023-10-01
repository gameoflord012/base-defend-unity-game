using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void SetIsMoving(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void TriggerJump()
    {
        animator.SetTrigger("jump");
    }
}
