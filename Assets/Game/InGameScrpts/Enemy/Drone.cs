using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyBase
{
    public float moveSpeed;
    PlayerControl playerControl;
    public GameObject drone;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);

        if (transform.position.y <= -7f || transform.position.y >= 7f) Destroy(gameObject);
        if (transform.position.x <= -7f || transform.position.x >= 7f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<EnemyBase>()?.OnDamage(hp);
            PlayerControl.Ondam = true;
            Destroy(gameObject);
        }
    }

    protected override void DieDestroy()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
