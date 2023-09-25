using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] Rigidbody2D rd;
    [SerializeField] float forceStrength;

    public void Add(Vector2 forceDir)
    {
        rd.AddForce(forceDir.normalized * forceStrength);
    }

    public void Add(Vector2 source, Vector2 destination)
    {
        Add(destination - source);
    }
}
