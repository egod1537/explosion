using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Player : Entity
{

    public static Vector3 Pos;

    public static Vector3 InstanceVector;

    private void Start()
    {

        Hp = PlayerManager.MaxHp;

        BossRegression.Init();

        PlayerManager.OnDamage.AddListener(() =>
        {

            BossRegression.addInstance(transform.localPosition);

        });

    }

    private void Update()
    {

        Pos = transform.localPosition;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {

            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet.team != team)
            {

                PlayerManager.Hp -= bullet.Damage;

                InstanceVector = new Vector3(
                    Mathf.Atan2(bullet.normal.y, bullet.normal.x),
                    transform.localPosition.x, 0f);

                PlayerManager.OnDamage.Invoke();

                Destroy(other.gameObject);

            }

        }

    }

}
