using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottomUIManager : MonoBehaviour
{

    public enum State
    {

        Upgrade,
        Rebirth,
        Skill,
        PvP,
        Cash

    }
    public State state;

    public Transform UIs;

    static Transform s_UIs;

    private void Start()
    {

        s_UIs = UIs;

    }

    public static void SwapUI(State _state) => s_UIs.DOLocalMoveX(((int)(_state)) * -Screen.width, 1f);

}
