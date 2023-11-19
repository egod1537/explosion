using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class OrbitFireBall : MonoBehaviour
{

    public int angle = 0;

    private void Update()
    {

        if (angle >= 360) angle = 0;

        transform.localPosition
            = BulletAction.getTrigonometric(angle) * 175;

        angle++;

    }

}
