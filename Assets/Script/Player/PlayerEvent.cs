using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class PlayerManager : MonoBehaviour
{

    public class DamageEvent : UnityEvent { }
    public static DamageEvent OnDamage = new DamageEvent();

    public class DeadEvent : UnityEvent { }
    public static DeadEvent OnDead = new DeadEvent();

}
