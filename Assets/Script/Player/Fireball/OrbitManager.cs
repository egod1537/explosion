using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class OrbitManager : MonoBehaviour
{

    GameObject Orbit;

    private void Awake()
    {

        Orbit = ResourceManager.Load("Bullet/Orbit");

        FireBall.OnChargeFireBall.AddListener(
            (int count) =>
            {

                AllDestroyOrbit();

                float oneAngle = 360f / count;

                for (int i = 0; i < count; i++)
                    CreateOrbit(oneAngle * i);

            });

        FireBall.OnUseFireBall.AddListener(
            (int count) =>
            {

                AllDestroyOrbit();

                float oneAngle = 360f / count;

                for (int i = 0; i < count; i++)
                    CreateOrbit(oneAngle * i);

            });

    }

    void CreateOrbit(float angle)
    {

        GameObject go = Instantiate(Orbit);

        go.transform.SetParent(transform, false);

        go.GetComponent<OrbitFireBall>().angle = (int) angle;

    }

    void AllDestroyOrbit()
    {

        for (int i =0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

}
