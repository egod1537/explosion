using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Mira_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public void Shot1(int angle)
    {

        BulletAction.Shot("Bullet2", angle,
        damage: boss.Damage);

    }

    public IEnumerator Pattern1(int angle)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {

            Shot1(angle);

            yield return wfx;

        }

    }

    public override void Pattern()
    {

        StartCoroutine(Pattern1((int)(Mathf.Atan2(Player.Pos.y, Player.Pos.x) * Mathf.Rad2Deg)));

    }
}
