using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;
using Random = UnityEngine.Random;

public partial class BossManager : MonoBehaviour
{

    public Transform parent_Boss;

    public Boss thisBoss;

    Dictionary<int, int> BossCount;

    private void Start()
    {

        StartCoroutine(MakeBoss());

        OnCreateBoss.AddListener((Boss boss) =>
        {

            thisBoss = boss;

        });

        OnDeadBoss.AddListener((Boss boss) =>
        {

            StageManager.MonsterCount--;

            PlayerManager.Gold += thisBoss.Gold;

            PlayerManager.SaveData();

            StartCoroutine(MakeBoss());

            Destroy(boss.gameObject);

        });

    }

    public IEnumerator MakeBoss()
    {

        yield return new WaitForSeconds(1f);

        int rand = (Random.Range(0, 3) + 1);

        if (StageManager.MonsterCount == 1) rand = 4;

        string path = "Boss/" + StageManager.stage + "/" + rand.ToString();

        GameObject go 
            = Instantiate(ResourceManager.Load(path));

        go.transform.SetParent(parent_Boss, false);

        Boss boss = go.GetComponent<Boss>();

        boss.GenerateStat();

    }

}
