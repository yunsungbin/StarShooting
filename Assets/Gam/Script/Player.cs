using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Doned
{
    public float xMin, xMax, zMin, zMax;
}
public class Player : MonoBehaviour
{
    public float speed;
    public float ti;
    public Doned doned;

    public GameObject Shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextTime;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextTime)
        {
            nextTime = fireRate + Time.time;
            Instantiate(Shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        GetComponent<Rigidbody>().velocity = move * speed;
    }
}
