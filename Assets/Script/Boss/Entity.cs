using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Entity : MonoBehaviour
{

    public enum Team
    {

        Player,
        Enemy
        
    }
    public Team team;

    public int Hp, MaxHp;
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {

            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet.team != team)
            {

                Hp -= bullet.Damage;

                Destroy(other.gameObject);

            }

        }

    }

}
