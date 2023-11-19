using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamLibrary
{

    public class UICore
    {

        ///@breif
        /// 1 : 플레이어 파이어볼 데미지 업그레이드
        /// 2 : 플레이어 파이어볼 쿨타임 감소 업그레이드
        /// 3 : 정령 데미지 업그레이드
        /// 4 : 정령 공격속도 업그레이드
        /// 5 : 환생 버튼
        public class BtnBuyUpgrade : UnityEvent<UpgradeManager.Upgrade> { }
        public static BtnBuyUpgrade OnBtnBuyUpgrade = new BtnBuyUpgrade();

    }

}
