using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPet : MonoBehaviour
{

    public Transform Player;
    public Vector3 addPos;

    private void Update()
    {

        transform.localPosition
            = Vector3.Lerp
            (transform.localPosition,
            Player.localPosition + addPos,
            0.05f);

    }

}
