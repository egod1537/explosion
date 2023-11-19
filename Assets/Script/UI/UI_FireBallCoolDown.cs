using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FireBallCoolDown : MonoBehaviour
{

    UISprite tr_sprite;

    private void Awake()
    {

        tr_sprite = GetComponent<UISprite>();

    }

    private void Start()
    {

        FireBall.OnChargingFireBall.AddListener(() =>
        {

            tr_sprite.fillAmount = 0;

            StartCoroutine(Ani());

        });

    }

    IEnumerator Ani()
    {

        float oneTime = PlayerManager.FireBall_Cooldown / 360f;

        WaitForSeconds wfs = new WaitForSeconds(oneTime);

        for (int i=1; i <= 360f; i++)
        {

            tr_sprite.fillAmount += 1f / 360f;

            yield return wfs;

        }

    }

}
