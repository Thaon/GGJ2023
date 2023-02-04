using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCompletionTracker : MonoBehaviour
{
    private float _completion = 0f;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, _completion * 90f));
    }

    public void SetCompletion(float comp)
    {
        _completion = comp;
    }
}
