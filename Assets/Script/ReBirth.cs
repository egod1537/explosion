using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;
public class ReBirth : MonoBehaviour
{
    public void ReBirthEvent()
    {
        PlayerManager.Jewel += StageManager.Floor;
        StageManager.Floor = 0;
        
    }
}
