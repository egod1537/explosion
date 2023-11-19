using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Skeleton_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public void Shot1(int angle)
    {

        BulletAction.Shot("Skeleton/Bullet", angle, 0.8f,
        team: Entity.Team.Enemy,
        damage: boss.Damage)
        .GetComponent<Bullet>()
        .setAnimation(BulletTween.AniType.Circle)
        .setAniProperty(
        _color: Color.green,
        aniCircle_Increase: 3f)
        .transform.position = transform.position;

    }

    public IEnumerator Pattern1(int angle)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.35f);

        for (int i = 0; i < 2; i++)
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
