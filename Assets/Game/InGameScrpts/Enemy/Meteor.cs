using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float Speed = 3f;
    private float time;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        time += Time.deltaTime;

        if (transform.position.y == -7f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && time > 0.1f)
        {
            PlayerControl.Ondam = true;
        }
    }
}
