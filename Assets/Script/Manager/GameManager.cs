using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class GameManager : MonoBehaviour
{

    private void OnDestroy()
    {

        BulletAction.Pool.Clear();

    }

}
