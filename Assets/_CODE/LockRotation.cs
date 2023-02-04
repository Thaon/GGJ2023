using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _transform.eulerAngles = new Vector3 (0, 0, 0);
    }
}
