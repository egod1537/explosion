using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJamLibrary;

using Upgrade = UpgradeManager.Upgrade;

public partial class PlayerManager : MonoBehaviour
{

    public const string data_path = "Player_Data";

    public static int Hp { get; set; }
    public static int MaxHp { get; set; }

    public static int Gold { get; set; }
    public static int Jewel {get; set;}
    public static int Rebirth_Count {get; set;}

    public static int FireBall_Damage {get; set;}
    public static int FireBall_MaxCount {get; set;}
    public static float FireBall_Cooldown {get; set;}

    public static int Demon_Damage { get; set; }
    public static float Demon_AttackSpeed { get; set; }

    public static Dictionary<Upgrade, int> Upgrade_State;

    bool isNoDamage = false;

    private void Awake()
    {

        LoadData();

        SaveManager.OnSave.AddListener(() => { SaveData(); LoadData(); });

        Hp = MaxHp;

        OnDead.AddListener(() =>
        {

            if (isNoDamage) return;

            Hp = MaxHp;

            StageManager.MonsterCount = StageManager.m_count;

            if (StageManager.Floor > 1) StageManager.Floor -= 2;
            else StageManager.Floor = 0;

            StageManager.GoStage();

            StageManager.SaveData();

            SceneLibrary.Load("main");

            isNoDamage = true;
            StartCoroutine(EndNoDamage());

        });

    }

    IEnumerator EndNoDamage()
    {

        yield return new WaitForSeconds(2f);

        isNoDamage = false;

    }

    private void Update()
    {

        if (isNoDamage) Hp = MaxHp;

        if (Hp <= 0 && !isNoDamage) OnDead.Invoke();

        if(Upgrade_State == null) ResetData();

    }

    public static void ResetData()
    {

        int count = 5;

        Upgrade_State = new Dictionary<Upgrade, int>();

        for (int i = 0; i < count; i++) Upgrade_State[((Upgrade)i)] = 1;

        SaveData();

    }

    public static void SaveData()
    {

        IOData.SaveData(data_path, "Gold", Gold);
        IOData.SaveData(data_path, "Jewel", Jewel);
        IOData.SaveData(data_path, "Rebirth_Count", Rebirth_Count);

        if (Upgrade_State == null) Upgrade_State = new Dictionary<Upgrade, int>();

        IOData.SaveData(data_path, "Upgrade", Upgrade_State);

    }

    public static void LoadData()
    {

        Gold =
            IOData.LoadData<int>(data_path, "Gold");
        Jewel =
            IOData.LoadData<int>(data_path, "Jewel");
        Rebirth_Count =
            IOData.LoadData<int>(data_path, "Rebirth_Count");

        Upgrade_State =
            IOData.LoadData<Dictionary<Upgrade, int>>(data_path, "Upgrade");

        if (Upgrade_State == null)
        {

            Upgrade_State = new Dictionary<Upgrade, int>();
            ResetData();

        }

        MaxHp =
            UpgradeManager.getValue(Upgrade.Hp, Upgrade_State[Upgrade.Hp]);
        MaxHp = 9999999;

        FireBall_Damage =
        UpgradeManager.getValue(Upgrade.FireballDamage, Upgrade_State[Upgrade.FireballDamage]);

        FireBall_Cooldown =
        UpgradeManager.getValue(Upgrade.FireballCooldown, Upgrade_State[Upgrade.FireballCooldown]);

        FireBall_MaxCount = 10;

        Demon_Damage =
        UpgradeManager.getValue(Upgrade.PetDamage, Upgrade_State[Upgrade.PetDamage]);

        Demon_AttackSpeed =
        UpgradeManager.getValue(Upgrade.PetAttackSpeed, Upgrade_State[Upgrade.PetAttackSpeed]);

    }

}