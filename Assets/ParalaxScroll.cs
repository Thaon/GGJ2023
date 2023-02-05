using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScroll : MonoBehaviour
{
    public int scrollSpeed;
    public float resetDist = 8.5f;
    private GameObject secondObject;
    void Start()
    {
        secondObject = transform.GetChild(0).gameObject;
        resetDist = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(scrollSpeed * Time.deltaTime, 0, 0);
        if(transform.localPosition.x > resetDist)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
