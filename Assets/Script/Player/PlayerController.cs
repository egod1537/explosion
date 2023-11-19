using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(0f, 10f)]
    public float _speed;

    [Range(0f, 1f)]
    public float q;

    private void Update()
    {
        
        if(Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            Vector3 localPos = touch.position -
                new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            transform.localPosition
                = Vector3.Lerp(transform.localPosition, 
                localPos, q);

        }

        transform.localPosition
            =
            new Vector3(
                Mathf.Clamp
                (transform.localPosition.x, -Screen.width * 0.5f, Screen.width * 0.5f),
                Mathf.Clamp
                (transform.localPosition.y, -Screen.height * 0.275f, -150f),
                0f);
    }

}
