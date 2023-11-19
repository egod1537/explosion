using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class StageManager : MonoBehaviour
{

    public class NextStage : UnityEvent<Stage> { }
    public static NextStage OnNextStage = new NextStage();

}
