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
        transform.Translate(scrollSpeed * Time.deltaTime, 0, 0);
        if(transform.localPosition.x > 8.5)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
