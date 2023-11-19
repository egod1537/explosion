using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MonsterCount : MonoBehaviour
{

    UILabel tr_lbl;

    private void Awake()
    {

        tr_lbl = GetComponent<UILabel>();

    }

    private void Update()
    {

        tr_lbl.text =
            StageManager.MonsterCount + "/" + StageManager.m_count;

    }

}
