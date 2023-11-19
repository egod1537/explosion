using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEffect : MonoBehaviour
{

    Animator trAnimator;

    private void Awake()
    {

        trAnimator = GetComponent<Animator>();

    }

    private void Start()
    {

    }

    public void AniStop() { trAnimator.speed = 0f; }

}
