using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;
using DG.Tweening;

public class Boss : Entity
{

    public int Gold;

    bool isNoDamage = false;

    private void Start()
    {

        transform.localPosition = new Vector3(0f, 850f, 0f);

        transform.DOLocalMoveY(0, 0.75f)
            .SetEase(Ease.OutSine);

        BossManager.OnCreateBoss.Invoke(this);
        Hp = MaxHp;

        isNoDamage = true;
        StartCoroutine(stopNoDamage());

    }

    private void Update()
    {

        if (isNoDamage) return;

        if (Hp <= 0) { BossManager.OnDeadBoss.Invoke(this); }

    }

    public void GenerateStat()
    {

        int floor = StageManager.Floor;

        MaxHp
            = (int)(8 * MathLibrary.my_pow(floor - 1, 3)) + 10;

        MaxHp += (int)(MaxHp * Random.Range(-0.25f, 0.25f));

        Gold
            = (int) (floor * 5 * MathLibrary.my_pow(floor - 1, 2) + 1000 + (floor - 1) * 1000);

        Gold
            += (int)(Gold * Random.Range(-0.25f, 0.25f));

        Damage
            = (int)(50 * MathLibrary.my_pow(floor - 1, 2) * 0.25f + 1);

    }

    IEnumerator stopNoDamage() { yield return new WaitForSeconds(1f); isNoDamage = false; }

}
