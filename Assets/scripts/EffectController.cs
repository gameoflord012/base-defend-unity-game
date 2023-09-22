using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public void PlayEffect(string effectName)
    {
        transform.Find(effectName).GetComponent<Animator>().SetTrigger("trigger");
    }

    private void Start()
    {
        
    }
}
