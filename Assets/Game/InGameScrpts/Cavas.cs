using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cavas : MonoBehaviour
{
    [Header("weapon")]
    public Image weaponImage;
    public Text weaponLevel;

    public Sprite[] weaponSprites;

    public hpGauge bossHp;
    public hpGauge playerHp;
    public hpGauge Fuel;

    public Skillbase healSkillGauge;
    public Skillbase BombSkillGauge;
    public Weapon weapon;
    public void InitWeapon(int index)
    {
        weaponImage.sprite = weaponSprites[index];
    }

    public void SetWeaponLevel()
    {
        weaponLevel.text = string.Format("LV.{0:0}", TempData.WeaponLevel);
    }
}
