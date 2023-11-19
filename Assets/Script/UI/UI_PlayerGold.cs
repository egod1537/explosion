using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerGold : MonoBehaviour
{

    UILabel tr_lbl;

    private void Awake()
    {

        tr_lbl = GetComponent<UILabel>();

    }

    private void Update()
    {

        tr_lbl.text = PlayerManager.Gold.ToString();

    }

}
