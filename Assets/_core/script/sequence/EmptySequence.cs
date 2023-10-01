using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySequence : SequenceBase
{
    protected override IEnumerator SequenceImplementation()
    {
        throw new System.NotImplementedException();
    }
}
