using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Demon : MonoBehaviour
{

    private void Start()
    {

        StartCoroutine(Attack());

    }

    IEnumerator Attack()
    {

        BulletAction.Shot("Bullet1", 90, 1f, team: 
            Entity.Team.Player, damage : PlayerManager.Demon_Damage)
            .transform.position = transform.position;

        SoundManager.EffectRun("pet_Attack");

        yield return 
            new WaitForSeconds(1f / PlayerManager.Demon_AttackSpeed);

        StartCoroutine(Attack());

    }

}
