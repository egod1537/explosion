using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Range(1, 10)]
    public float speed;

    Rigidbody tr_rig;

    private void Awake()
    {

        tr_rig = GetComponent<Rigidbody>();

    }

    Vector3 _axis;

    void Update()
    {

        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");

        if (_axis.x != 0 || _axis.y != 0)
            tr_rig.MovePosition(transform.position + _axis * speed * Time.deltaTime);

    }
}
