using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BlindFloor : MonoBehaviour
{

    public TweenAlpha Blind;

    private void Awake()
    {

        StageManager.OnNextStage.AddListener((StageManager.Stage stage) =>
        {

            StartCoroutine(AniBlind());

        });

    }

    private void Start()
    {

        ShowBlind();

    }

    public IEnumerator AniBlind()
    {

        CloseBlind();

        yield return new WaitForSeconds(0.75f);

        ShowBlind();

    }

    public void ShowBlind()
    {

        Blind.from = 1;
        Blind.to = 0;

        Blind.enabled = true;
        Blind.ResetToBeginning();

    }

    public void CloseBlind()
    {

        Blind.from = 0;
        Blind.to = 1;

        Blind.enabled = true;
        Blind.ResetToBeginning();

    }
}
