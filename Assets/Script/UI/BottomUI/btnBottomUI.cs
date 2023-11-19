using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnBottomUI : MonoBehaviour
{

    public BottomUIManager.State state;

    public void Active() { BottomUIManager.SwapUI(state); }

}
