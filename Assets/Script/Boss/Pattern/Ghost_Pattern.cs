using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Ghost_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public void Shot1(int angle)
    {

        BulletAction.Shot("Bullet1", angle, 0.8f,
        team: Entity.Team.Enemy,
        damage: boss.Damage)
        .GetComponent<Bullet>()
        .setAnimation(BulletTween.AniType.Bounce)
        .setAniProperty(
        _color: Color.white,
        aniBounce_Increase: 5,
        aniBounce_MinScale: 1,
        aniBounce_MaxScale: 2f)
        .transform.position = transform.position;

    }

    public IEnumerator Pattern1()
    {

        WaitForSeconds wfx = new WaitForSeconds(0.15f);

        for (int i = 0; i < 10; i++)
        {

            Shot1(Random.Range(220, 320));

            yield return wfx;

        }

    }

    public override void Pattern()
    {

        StartCoroutine(Pattern1());

    }
}
