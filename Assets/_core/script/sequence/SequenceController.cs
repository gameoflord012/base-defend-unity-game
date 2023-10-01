using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    [SerializeField] List<SequenceBase> sequences;

    public void PlaySequences()
    {
        foreach (var sequence in sequences)
        {
            StartCoroutine(sequence.GetSequence());
        }
    }
}
