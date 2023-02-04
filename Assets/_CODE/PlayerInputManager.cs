using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    #region member variables

    public float _speed;
    public MovementManager _movementManager;
    public AnimationManager _animationManager;

    #endregion

    void Update()
    {
        Vector3 movement = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
            );
        _movementManager.Move(movement, _speed);
        if(_animationManager){
            _animationManager.SetMovement(movement);
        }
    }
}
