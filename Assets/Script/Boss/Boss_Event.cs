using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class BossManager : MonoBehaviour
{

    public class CreateBoss : UnityEvent<Boss> { }
    public static CreateBoss OnCreateBoss = new CreateBoss();

    public class DeadBoss : UnityEvent<Boss> { }
    public static DeadBoss OnDeadBoss = new DeadBoss();

}

