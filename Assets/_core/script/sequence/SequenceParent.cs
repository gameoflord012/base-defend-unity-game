
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceParent : MonoBehaviour
{
    public void PlaySequence(string sequencePlayerGameobjectName)
    {
        transform.Find(sequencePlayerGameobjectName)?.GetComponent<SequencePlayer>().PlaySequences();
    }
}
