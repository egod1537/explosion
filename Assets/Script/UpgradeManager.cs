using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class UpgradeManager : MonoBehaviour
{

    public enum Upgrade
    {

        FireballDamage,
        FireballCooldown,
        Hp,
        PetDamage,
        PetAttackSpeed

    }

    Dictionary<Upgrade, int> MaxLevel;

    private void Awake()
    {

        MaxLevel = new Dictionary<Upgrade, int>();

        MaxLevel[Upgrade.FireballDamage] = 100;
        MaxLevel[Upgrade.FireballCooldown] = 20;
        MaxLevel[Upgrade.Hp] = 100;
        MaxLevel[Upgrade.PetDamage] = 100;
        MaxLevel[Upgrade.PetAttackSpeed] = 100;

        UICore.OnBtnBuyUpgrade.AddListener((Upgrade _up) =>
        {

            if (PlayerManager.Upgrade_State[_up] >= MaxLevel[_up]) return;

            int level = PlayerManager.Upgrade_State[_up];

            if (PlayerManager.Gold < getPrice(_up, level)) return;

            PlayerManager.Upgrade_State[_up]++;

            PlayerManager.Gold -= getPrice(_up, level);

            PlayerManager.SaveData();
            PlayerManager.LoadData();

            if (_up == Upgrade.Hp) PlayerManager.Hp = PlayerManager.MaxHp;

        });

    }

    public static float getPlayerValue(Upgrade _up)
    {

        switch (_up)
        {

            case Upgrade.FireballDamage:

                return PlayerManager.FireBall_Damage;

            case Upgrade.FireballCooldown:

                return PlayerManager.FireBall_Cooldown;

            case Upgrade.Hp:

                return PlayerManager.MaxHp;

            case Upgrade.PetDamage:

                return PlayerManager.Demon_Damage;

            case Upgrade.PetAttackSpeed:

                return PlayerManager.Demon_AttackSpeed;

        }

        return 0;

    }

    public static int getValue(Upgrade _up, int level)
    {

        switch (_up)
        {

            case Upgrade.FireballDamage:

                return getFireBallDamage(level);

            case Upgrade.FireballCooldown:

                return getFireBallCooldown(level);

            case Upgrade.Hp:

                return getHp(level);

            case Upgrade.PetDamage:

                return getPetDamage(level);

            case Upgrade.PetAttackSpeed:

                return getPetAttackSpeed(level);

        }

        return 0;

    }

    public static int getPrice(Upgrade _up, int level)
    {

        switch (_up)
        {

            case Upgrade.FireballDamage:

                return getFireBallDamage_Price(level);

            case Upgrade.FireballCooldown:

                return getFireBallCooldown_Price(level);

            case Upgrade.Hp:

                return getHp_Price(level);

            case Upgrade.PetDamage:

                return getPetDamage_Price(level);

            case Upgrade.PetAttackSpeed:

                return getPetAttackSpeed_Price(level);

        }

        return 0;

    }

    public static int getFireBallDamage(int level)
    { return (int)(25 * MathLibrary.my_pow(level - 1, 2) + 10); }
    public static int getFireBallDamage_Price(int level)
    { return (int)(12 * MathLibrary.my_pow(level - 1, 3) + 100 + (level - 1) * 200); }

    public static int getHp(int level)
    { return (int)(50 * MathLibrary.my_pow(level - 1, 3) + 10); }
    public static int getHp_Price(int level)
    { return (int)(3 * level * MathLibrary.my_pow(level - 1, 3) + 100); }

    public static int getFireBallCooldown(int level)
    { return (int)(0.257* (20 - level) + 0.1f); }
    public static int getFireBallCooldown_Price(int level)
    { return (int)(100 * (level - 1) * MathLibrary.my_pow(level - 1, 3) + 10000); }

    public static int getPetDamage(int level)
    { return (int)(MathLibrary.my_pow(level-1, 2) + 5); }
    public static int getPetDamage_Price(int level)
    { return (int)(1000 * (level - 1) + MathLibrary.my_pow(level - 1, 3) + (level - 1) * 1000 + 1000); }

    public static int getPetAttackSpeed(int level)
    { return (int)(Mathf.Round(15 * Mathf.Log10(level)) + 2); }
    public static int getPetAttackSpeed_Price(int level)
    { return (int)(10 * MathLibrary.my_pow(level - 1, 3) + 500); }

}
