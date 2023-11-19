using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UpgradePrice : MonoBehaviour
{

    public UpgradeManager.Upgrade upgrade;

    UILabel tr_lbl;

    private void Awake()
    {

        tr_lbl = GetComponent<UILabel>();

    }

    void Update()
    {

        if (PlayerManager.Upgrade_State == null) return;

        tr_lbl.text =
            UpgradeManager.getPrice(upgrade,
            PlayerManager.Upgrade_State[upgrade])
            .ToString();

    }
}
