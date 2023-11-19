using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_Move : MonoBehaviour
{

    public int Speed;

    private void Update()
    {
        
        if(transform.localPosition.y <= -Screen.height)
            transform.localPosition = Vector3.zero;

        transform.localPosition += new Vector3(0f, -1f, 0f) * Speed * Time.deltaTime;

    }

}
