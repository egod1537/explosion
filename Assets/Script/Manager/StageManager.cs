using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public partial class StageManager : MonoBehaviour
{

    public enum Stage
    {

        Grass,
        Grave,
        Desert,

    }

    public const int m_count = 10;

    public static Stage stage;

    public static int MonsterCount = m_count;

    public static int Floor = 1;

    bool isNext = false;

    private void Awake()
    {

        SaveManager.OnSave.AddListener(() => { SaveData(); LoadData(); });

        LoadData();

    }

    private void Start()
    {

        OnNextStage.AddListener(
            (Stage stage) => 
            {

                if (isNext) return;

                MonsterCount = m_count;

                Floor++;

                isNext = true;
                StartCoroutine(noNext());

            }
            );

    }

    IEnumerator noNext()
    {

        yield return new WaitForSeconds(2f);

        isNext = false;

    }

    private void Update()
    {

        if (MonsterCount <= 0 && !isNext) GoStage();

    }

    public static void SaveData()
    {

        IOData.SaveData("Stage_Data", "Floor", Floor);
        IOData.SaveData("Stage_Data", "Stage", (int) stage);
        IOData.SaveData("Stage_Data", "MonsterCount", MonsterCount);

    }

    public static void LoadData()
    {

        Floor
            = IOData.LoadData<int>("Stage_Data", "Floor");
        stage
            = (Stage) IOData.LoadData<int>("Stage_Data", "Stage");
        MonsterCount
            = IOData.LoadData<int>("Stage_Data", "MonsterCount");

    }

    public static void GoStage()
    {

        int Rand = UnityEngine.Random.Range(0, 3);

        while (Rand == (int)stage)
            Rand = UnityEngine.Random.Range(0, 3);

        stage = (Stage)Rand;

        OnNextStage.Invoke((Stage)Rand);

    }

}
