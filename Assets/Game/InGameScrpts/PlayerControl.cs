using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Done
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float ti;
    public Done done;

    public GameObject shot;
    public Transform shotSpawn;
    public float fire;

    private float nextFire;

    public float MaxHp;
    public float MaxFuel;

    public float hp;
    public float fuel;
    public int weaponLevel = 0;

    private float dmg = 2;
    public static bool Ondam = false;

    InGameManager inGameManager;

    public static bool barier = false;
    public static bool bossTime = false;

    public float fueltime = 0;
    void Start()
    {
        hp = MaxHp;
        fuel = MaxFuel;

        weaponLevel = TempData.WeaponLevel;
        InGameManager.Instance.cavas.SetWeaponLevel();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fire;
            BulletShoot();
        }
        if (barier == false && Input.GetKey(KeyCode.F1))
        {
            barier = true;
        }
        if (barier == true && Input.GetKey(KeyCode.F2))
        {
            barier = false;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            bossTime = true;
        }
        if(BossTram.next == true)
        {
            NextScene();
        }
        SetUI();
        Fueldown();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, v, 0);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        GetComponent<Rigidbody2D>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, done.xMin, done.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, done.zMin, done.zMax),
            0.0f);

        //GetComponent<Rigidbody2D>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody2D>().velocity.x * -ti);
    }

    public void Damage()
    {
        if (Ondam == true && barier == false)
        {
            hp -= dmg;
            Ondam = false;
        }

        if (hp <= dmg)
        {
            SceneManager.LoadScene("Ranking");
        }
    }

    public void Fueldown()
    {
        if(fueltime > 1)
        {
            fuel--;
            fueltime = 0;
        }
        fueltime += Time.deltaTime;
    }

    public virtual void WeaponLevelUp()
    {
        weaponLevel++;
        TempData.WeaponLevel = weaponLevel;
        InGameManager.Instance.cavas.SetWeaponLevel();
    }

    void BulletShoot()
    {
        if (weaponLevel >= 3)
        {
            Instantiate(shot, transform.position + new Vector3(-0.75f, -0.25f), Quaternion.Euler(0, 0, 90 + 15));
            Instantiate(shot, transform.position + new Vector3(0.75f, -0.25f), Quaternion.Euler(0, 0, 90 - 15));
        }
        if (weaponLevel >= 2)
        {
            Instantiate(shot, transform.position + new Vector3(-0.5f, 0f), Quaternion.Euler(0, 0, 90));
            Instantiate(shot, transform.position + new Vector3(0.5f, 0f), Quaternion.Euler(0, 0, 90));
        }
        if (weaponLevel >= 1)
        {
            Instantiate(shot, transform.position + new Vector3(-0.15f, 0.5f), Quaternion.Euler(0, 0, 90));
            Instantiate(shot, transform.position + new Vector3(0.15f, 0.5f), Quaternion.Euler(0, 0, 90));
        }
        else
        {
            Instantiate(shot, transform.position + new Vector3(0f, 0.5f), Quaternion.Euler(0, 0, 90));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ebullet") || collision.CompareTag("Meteor"))
        {
            Damage();
        }
    }

    public void HpRecover(float amount = 10f)
    {
        hp += amount;
        if (hp > MaxHp) hp = MaxHp;
    }

    public void FuelRecover(float amount = 10f)
    {
        fuel += amount;
        if (fuel > MaxFuel) fuel = MaxFuel;
    }

    void SetUI()
    {
        InGameManager.Instance.cavas.playerHp.SetGauge(hp / MaxHp);
        InGameManager.Instance.cavas.Fuel.SetGauge(fuel / MaxFuel);
    }

    private void NextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int curScene = scene.buildIndex;
        int nextScene = curScene + 1;
        SceneManager.LoadScene(nextScene);
    }
}
