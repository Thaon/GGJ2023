using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    #region member variables

    private Rigidbody _rb;

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir, float speed)
    {
        dir = dir.normalized;

        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }
}
