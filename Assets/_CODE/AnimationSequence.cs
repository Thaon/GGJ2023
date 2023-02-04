using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSequence : MonoBehaviour
{
    public List<Sprite> _sprites;

    public async void Animate()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            await System.Threading.Tasks.Task.Delay(500);
            GetComponent<SpriteRenderer>().sprite = _sprites[i];
        }
    }
}
