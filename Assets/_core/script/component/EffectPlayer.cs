using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectPlayer : MonoBehaviour
{
    public UnityEvent onEffectStopPlaying;
    public UnityEvent onEffectStartPlaying;

    bool isPlaying = false;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void PlayEffect()
    {
        animator.SetTrigger("trigger");

        isPlaying = true;
        onEffectStartPlaying.Invoke();
    }

    private void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("no effect"))
        {
            if(isPlaying == true)
            {
                onEffectStopPlaying.Invoke();
                isPlaying = false;
            }
        }
    }
}
