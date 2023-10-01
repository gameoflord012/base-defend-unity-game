using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TargetGetAttackedSequence : MonoBehaviour
{
    [SerializeField] Targeting theTarget;

    [SerializeField] UnityEvent onSequencePlay;

    EnemyController controller;
    AddForce addForce;
    ColorChanger colorChanger;

    Vector2 lastAttackedDirection;

    private void Start()
    {
        addForce = GetComponent<AddForce>();
        colorChanger = GetComponent<ColorChanger>();
        controller = transform.root.GetComponentInChildren<EnemyController>();
    }
    private void OnEnable()
    {
        theTarget.onGetAttackDirection.AddListener(SetLastAttackedDirection);
    }

    private void OnDisable()
    {
        theTarget.onGetAttackDirection.RemoveListener(SetLastAttackedDirection);
    }

    public void SetLastAttackedDirection(Vector2 last)
    {
        lastAttackedDirection = last;
    }
    public void PlaySequence()
    {
        onSequencePlay.Invoke();
        StartCoroutine(Sequence());
        StartCoroutine(colorChanger.ChangeColor());
    }

    IEnumerator Sequence()
    {
        controller.enabled = false;
        yield return new WaitForNextFrameUnit();
        addForce.Add(lastAttackedDirection);
        yield return new WaitForSeconds(1f);
        controller.enabled = true;
    }
}
