using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class ColorChanger : SequenceBase
{
    [SerializeField] float duration;
    [SerializeField] float frequencyPerSecond;
    [SerializeField] Color destinationColor;
    [SerializeField] SpriteRenderer sprite;

    Color originalColor;

    private void Start()
    {
        originalColor = sprite.color;
    }

    protected override IEnumerator SequenceImplementation()
    {
        float timer = 0;

        while (timer < duration)
        {
            sprite.color = destinationColor;

            yield return new WaitForSeconds(1 / frequencyPerSecond / 2);

            sprite.color = originalColor;

            yield return new WaitForSeconds(1 / frequencyPerSecond / 2);

            timer += 1 / frequencyPerSecond;
        }

        sprite.color = originalColor;
    }
}
