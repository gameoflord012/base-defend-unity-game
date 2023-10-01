using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceController : MonoBehaviour
{
    [SerializeField] UnityEvent onSequencesStartToPlay;
    [SerializeField] List<SequenceBase> sequences;

    public void PlaySequences()
    {
        onSequencesStartToPlay?.Invoke();
        foreach (var sequence in sequences)
        {
            StartCoroutine(sequence.GetSequence());
        }
    }
}
