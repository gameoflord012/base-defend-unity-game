using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    [SerializeField] Rigidbody2D rd;

    public void ShouldFreeze(bool should)
    {
        if(should)
        {
            rd.constraints |= RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rd.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        }
    }

    public void Freeze()
    {
        ShouldFreeze(true);
    }

    public void UnFreeze()
    {
        ShouldFreeze(false);
    }
}
