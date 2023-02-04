using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    #region member variables

    private Rigidbody _rb;
    private Animator animator;

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 dir, float speed)
    {
        dir = dir.normalized;

        bool stationary = dir.x == 0 && dir.y == 0;
        animator.SetBool("stationary", stationary);
        if(!stationary){
            Debug.Log("X: " + dir.x + " Y: " + dir.y);
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
        }
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }
}
