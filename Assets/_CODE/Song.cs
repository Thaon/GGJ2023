using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Song", order = 1)]
public class Song : ScriptableObject
{
    public List<float> pitches;
    public List<Sprite> sprites;
}
