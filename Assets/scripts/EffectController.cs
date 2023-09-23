using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public void PlayEffect(string effectName)
    {
        transform.Find(effectName).GetComponent<EffectPlayer>().PlayEffect();
    }

    private void Start()
    {
        
    }
}
