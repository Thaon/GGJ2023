using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivityManager : MonoBehaviour
{
    public List<MonoBehaviour> _behaviors;

    public void Lock()
    {
        _behaviors.ForEach(b => b.enabled = false);
    }

    public void Unlock()
    {
        _behaviors.ForEach(b => b.enabled = true);
    }
}

