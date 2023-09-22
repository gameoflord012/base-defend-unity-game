using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float healthPercentage = 1f;

    RectTransform rectTransform;

    public void SetHealthPercent(float percent)
    {
        healthPercentage = percent;
    }

    private void Start()
    {
        rectTransform = transform.Find("Green").GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.localScale = new Vector3(healthPercentage, rectTransform.localScale.y, rectTransform.localScale.z);
    }
}
