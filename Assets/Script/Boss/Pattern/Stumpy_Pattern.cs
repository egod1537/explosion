using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Stumpy_Pattern : BossPattern
{
    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;

    }

    public IEnumerator ShotLine(Vector3 pos, int _angle)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.2f);

        for (int i = 0; i < 5; i++)
        {

            Shot1(pos, _angle);

            yield return wfx;

        }

    }

    public void Shot1(Vector3 _pos, int angle)
    {

        BulletAction.Shot("Bullet1", angle, 0.5f,
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
            ShotLine(
                new Vector3(Random.Range(-200f, 200f), 300f, 0f), 
                (int)Random.Range(220f, 340f)));

            yield return wfx;

        }

    }

    public override void Pattern()
    {

        StartCoroutine(Pattern1());

    }
}
