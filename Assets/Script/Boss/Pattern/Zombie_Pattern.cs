using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Zombie_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public IEnumerator ShotLine(Vector3 pos)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.15f);

        for (int i = 0; i < 5; i++)
        {

            Shot1(pos);

            yield return wfx;

        }

    }

    public void Shot1(Vector3 _pos)
    {

        BulletAction.Shot("Bullet1", 270, 0.5f,
            team: Entity.Team.Enemy,
            damage: boss.Damage)
            .GetComponent<Bullet>()
            .setAnimation(BulletTween.AniType.None)
            .setAniProperty(
            _color: Color.gray)
            .transform.localPosition = _pos;

    }

    public IEnumerator Pattern1()
    {

        WaitForSeconds wfx = new WaitForSeconds(0.25f);

        for (int i = 0; i < 3; i++)
        {

            StartCoroutine(
            ShotLine(new Vector3(Random.Range(-500f, 500f), 300f, 0f)));

            yield return wfx;

        }

    }

    public override void Pattern()
    {

        StartCoroutine(Pattern1());

    }
}
