using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    #region member variables

    public float _speed;
    public MovementManager _movementManager;

    #endregion

    void Update()
    {
        Vector3 movement = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
            );
        _movementManager.Move(movement, _speed);
    }
}
