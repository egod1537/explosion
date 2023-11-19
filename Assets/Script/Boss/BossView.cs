using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossView : MonoBehaviour
{

    public Transform Hp_bar;

    public UILabel lbl_Hp;

    Boss boss;

    private void Awake()
    {

        boss = GetComponent<Boss>();

    }

    private void Update()
    {

        if (boss.Hp <= 0)
        {

            Hp_bar.DOScaleX(0f, 0.5f);
            return;

        }

        Hp_bar.DOScaleX(
            (float) boss.Hp / boss.MaxHp, 0.5f);

        lbl_Hp.text =
            boss.Hp.ToString();

    }

}
