using System.Collections;
using System.Collections.Generic;
using GameJamLibrary;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    public bool isTest = false;

    public GameObject Player;

    private void Awake()
    {

        BossRegression.Init();

    }

    private void Start()
    {
    }

    public int x;

    private void Update()
    {

        if (isTest)
        {

            Debug.Log(BossRegression.getRegressionPos(x));

            isTest = false;

        }

    }

}
