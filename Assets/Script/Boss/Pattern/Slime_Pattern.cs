using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Slime_Pattern : BossPattern
{

    Boss boss;

    private void Awake()
    {

        boss = transform.parent.GetComponent<Boss>();

        PatterDelay = 5;
        
    }

    public void Shot1(int angle)
    {
        
        BulletAction.Shot("Slime/Bullet1", angle, 0.8f,
        team: Entity.Team.Enemy,
        damage: boss.Damage)
        .GetComponent<Bullet>()
        .setAnimation(BulletTween.AniType.Bounce)
        .setAniProperty(
        _color: Color.green,
        aniBounce_Increase: 5,
        aniBounce_MinScale: 1,
        aniBounce_MaxScale: 2f)
        .transform.position = transform.position;

    }
    
    public IEnumerator Pattern1(int angle)
    {

        WaitForSeconds wfx = new WaitForSeconds(0.35f);

        for(int i=0; i < 5; i++)
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
