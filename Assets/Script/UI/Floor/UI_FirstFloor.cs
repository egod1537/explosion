using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FirstFloor : MonoBehaviour
{

    public GameObject ui_floor;

    // Start is called before the first frame update
    void Start()
    {

        ui_floor.GetComponent<UISprite>().spriteName
            = ((int)StageManager.stage + 1).ToString();

    }

}
