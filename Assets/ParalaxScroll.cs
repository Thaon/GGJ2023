using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScroll : MonoBehaviour
{
    public int scrollSpeed;
    private GameObject secondObject;
    void Start()
    {
        secondObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(scrollSpeed * Time.deltaTime, 0, 0, Space.World);
        if(transform.position.x > 15)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
