using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool stationary = moveDirection.x == 0 && moveDirection.y == 0;
        animator.SetBool("stationary", stationary);
        if(!stationary){
            //Debug.Log("X: " + moveDirection.x + " Y: " + moveDirection.y);
            animator.SetFloat("X", moveDirection.x);
            animator.SetFloat("Y", moveDirection.y);
        }
        
    }

    public void SetMovement(Vector3 movement){
        moveDirection = movement;
    }
}
