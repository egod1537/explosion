using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Oak_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public void Shot1(int angle)
    {

        BulletAction.Shot("Bullet4", angle, 1f,
        team: Entity.Team.Enemy,
        damage: boss.Damage)
        .GetComponent<Bullet>()
        .setAnimation(BulletTween.AniType.Circle)
        .setAniProperty(
        _color: new Color(0.3593f, 0.1176f, 0f, 1f),
        aniCircle_Increase : 10)
        .transform.position = transform.position;

    }

    public IEnumerator Pattern1(int angle)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.15f);

        for (int i = 0; i < 8; i++)
        {

            Shot1(angle); Shot1(angle - 45); Shot1(angle + 45);

            yield return wfx;

        }

    }

    public override void Pattern()
    {

        StartCoroutine(Pattern1((int)Random.Range(235f, 295f)));

    }
}
