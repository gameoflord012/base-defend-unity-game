using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderBroadcaster : MonoBehaviour
{
    public UnityEvent<Collider2D, Collider2D> onColliderEnter2D;
    public UnityEvent<Collider2D, Collider2D> onColliderExit2D;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onColliderEnter2D.Invoke(GetComponent<Collider2D>(), collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onColliderExit2D.Invoke(GetComponent<Collider2D>(), collision);

    }
}
