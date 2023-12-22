using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrool : MonoBehaviour
{
    public float scrolSpeed;
    public float tSize;

    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrolSpeed, tSize);
        transform.position = startpos + Vector3.forward * newPos;
    }
}
