using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SequenceBase : MonoBehaviour
{
    [SerializeField] UnityEvent onSequencePlay;
    protected abstract IEnumerator SequenceImplementation();
    public IEnumerator GetSequence()
    {
        onSequencePlay?.Invoke();
        return SequenceImplementation();
    }
}
