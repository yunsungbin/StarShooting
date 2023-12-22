using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EnemyBase
{
    public GameObject bullet;
    public float Speed;
    public float shotDelay;
    float curDelay;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        if(shotDelay <= curDelay)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
            curDelay = 0f;
        }
        transform.Translate(Vector3.down * Time.deltaTime * Speed);
        curDelay += Time.deltaTime;

        if (transform.position.y <= -7f || transform.position.y >= 7f) Destroy(gameObject);
        if (transform.position.x <= -7f || transform.position.x >= 7f) Destroy(gameObject);

    }

    protected override void DieDestroy()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        for (int i = 0; i < 360; i += 360 / 10)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
        }
        Destroy(gameObject);
    }
}
