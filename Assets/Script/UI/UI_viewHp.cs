using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_viewHp : MonoBehaviour
{
    public Transform Hp_bar;

    public UILabel lbl_Hp;

    private void Update()
    {

        if (PlayerManager.Hp <= 0)
        {

            Hp_bar.DOScaleX(0f, 0.5f);
            return;

        }

        Hp_bar.DOScaleX(
            (float)PlayerManager.Hp / PlayerManager.MaxHp, 0.5f);

        lbl_Hp.text =
            PlayerManager.Hp.ToString();

    }
}
