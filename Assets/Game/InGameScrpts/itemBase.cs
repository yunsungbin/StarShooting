using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class itemBase : MonoBehaviour
{
    public float Speed;
    bool Get = false;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        if(transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Get)
        {
            TriggerEvent(collision);
            Get = true;
            Destroy(gameObject);
        }
    }

    protected abstract void TriggerEvent(Collider2D collision);
}
