using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Upgradebar : MonoBehaviour
{

    public bool isOpen = false;

    public Transform go_bar;

    Vector3 OpenPos = new Vector3(0f, 1000f, 0f),
            ClosePos = Vector3.zero;

    public void Active()
    {

        if (!isOpen) Open();
        else Close();

    }

    public void Open()
    {

        transform
            .DOLocalMove(OpenPos, .5f)
            .SetEase(Ease.OutBack);

        go_bar.localEulerAngles = new Vector3(0f, 0f, 180f);

        isOpen = !isOpen;

    }

    public void Close()
    {

        transform
            .DOLocalMove(ClosePos, .75f)
            .SetEase(Ease.OutBack);

        go_bar.localEulerAngles = Vector3.zero;

        isOpen = !isOpen;

    }

}
