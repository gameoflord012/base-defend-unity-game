using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AddForceOnTargetting : SequenceBase
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

    protected override IEnumerator SequenceImplementation()
    {
        controller.enabled = false;
        yield return new WaitForNextFrameUnit();
        addForce.Add(theTarget.GetLastAttackedDirection());
        yield return new WaitForSeconds(1f);
        controller.enabled = true;
    }
}
