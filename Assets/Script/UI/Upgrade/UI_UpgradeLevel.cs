using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UpgradeLevel : MonoBehaviour
{

    public UpgradeManager.Upgrade upgrade;

    UILabel tr_label;

    private void Awake()
    {

        tr_label = GetComponent<UILabel>();

    }

    void Update()
    {

        if (PlayerManager.Upgrade_State == null) return;

        if (PlayerManager.Upgrade_State.ContainsKey(upgrade))
            tr_label.text
            = PlayerManager.Upgrade_State[upgrade].ToString()
            + " / "
            + UpgradeManager.getPlayerValue(upgrade);

    }
}
