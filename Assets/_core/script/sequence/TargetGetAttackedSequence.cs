using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TargetGetAttackedSequence : SequenceBase
{
    [SerializeField] Targeting theTarget;

    EnemyController controller;
    AddForce addForce;
    ColorChanger colorChanger;

    private void Start()
    {
        addForce = GetComponent<AddForce>();
        colorChanger = GetComponent<ColorChanger>();
        controller = transform.root.GetComponentInChildren<EnemyController>();
    }
    public void PlaySequence()
    {
        StartCoroutine(GetSequence());
        StartCoroutine(colorChanger.ChangeColor());
    }

    protected override IEnumerator SequenceImplementation()
    {
        controller.enabled = false;
        yield return new WaitForNextFrameUnit();
        addForce.Add(theTarget.GetLastAttackedDirection());
        yield return new WaitForSeconds(1f);
        controller.enabled = true;
    }
}
