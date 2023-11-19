using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class FireBall : MonoBehaviour
{

    public class ChargeFireBall : UnityEvent<int> { }
    public static ChargeFireBall OnChargeFireBall = new ChargeFireBall();

    public class UseFireBall : UnityEvent<int> { }
    public static UseFireBall OnUseFireBall = new UseFireBall();

    public class ChargingFireBall : UnityEvent { }
    public static ChargingFireBall OnChargingFireBall = new ChargingFireBall();

}
