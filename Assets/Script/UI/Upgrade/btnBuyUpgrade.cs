using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class btnBuyUpgrade : MonoBehaviour
{

    public UpgradeManager.Upgrade upgrade;

    public void btnEvnet()
    {

        UICore.OnBtnBuyUpgrade.Invoke(upgrade);
        SoundManager.EffectRun("btn_Click");

    }

}
