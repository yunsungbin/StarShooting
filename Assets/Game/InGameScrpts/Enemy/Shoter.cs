using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoter : EnemyBase
{
    public GameObject bullet;
    public float speed;
    public float Delay;
    public float curDelay = 0;
    public GameObject explosion;

    void Update()
    {
        if(Delay <= curDelay)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(180, 0, 60));
            Instantiate(bullet, transform.position, Quaternion.Euler(180, 0, 80));
            Instantiate(bullet, transform.position, Quaternion.Euler(180, 0, 100));
            curDelay = 0f;
        }

        transform.Translate(Vector3.down * Time.deltaTime * speed);
        curDelay += Time.deltaTime;

        if (transform.position.y <= -7f || transform.position.y >= 7f) Destroy(gameObject);
        if (transform.position.x <= -7f || transform.position.x >= 7f) Destroy(gameObject);
    }

    protected override void DieDestroy()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        for (int i = 0; i < 360; i += 360 / 5)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
        }
        Destroy(gameObject);
    }
}
