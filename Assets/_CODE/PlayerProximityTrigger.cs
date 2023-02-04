using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerProximityTrigger : MonoBehaviour
{
    #region member variables

    public float _triggerDistance;
    public UnityEvent OnTriggerEnter;

    private Transform _player;
    private bool _triggered = false;

    #endregion


    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!_triggered && Vector3.Distance(transform.position, _player.position) < _triggerDistance)
        {
            OnTriggerEnter?.Invoke();
            _triggered = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _triggerDistance);
    }
}
