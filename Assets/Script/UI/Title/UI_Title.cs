using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Title : MonoBehaviour
{

    public Transform ch1, ch2;

    private void Start()
    {

        ch1.DOLocalMove(new Vector3(0f, -200f, 0f), 0.8f)
            .SetEase(Ease.OutQuad);

        ch2.DOLocalMove(new Vector3(0f -325f, 0f), 0.8f)
            .SetEase(Ease.OutQuad);

    }

}
