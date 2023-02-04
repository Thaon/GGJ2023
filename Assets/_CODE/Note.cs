using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Note", order = 2)]
public class Note : ScriptableObject
{
    public float pitch;
    public Sprite sprite;
    public int id;
}
