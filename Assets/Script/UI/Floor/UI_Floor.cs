using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Floor : MonoBehaviour
{

    TweenAlpha tr_twalpha;

    private void Awake()
    {

        tr_twalpha = GetComponent<TweenAlpha>();

    }

    private void Start()
    {

        transform.GetChild(0).GetComponent<UILabel>().text
            = StageManager.Floor + "F";

        transform.localPosition = new Vector3(-100f, 0f, 0f);

        transform
            .DOLocalMoveX(0f, 1f)
            .SetEase(Ease.OutSine);

        FadeIn();

        StageManager.OnNextStage.AddListener(
            (StageManager.Stage stage) =>
        {

            transform
                .DOLocalMoveX(100f, 1f)
                .SetEase(Ease.OutSine)
                .OnComplete(() => Destroy(gameObject));

            FadeOut();

            GameObject go = Instantiate(gameObject);

            go.transform.SetParent(transform.parent, false);

            go.GetComponent<UISprite>().spriteName
                = ((int)stage + 1).ToString();

        });

    }

    public void FadeIn()
    {

        tr_twalpha.from = 0f;
        tr_twalpha.to = 1f;

        tr_twalpha.enabled = true;
        tr_twalpha.ResetToBeginning();

    }

    public void FadeOut()
    {

        tr_twalpha.from = 1f;
        tr_twalpha.to = 0f;

        tr_twalpha.enabled = true;
        tr_twalpha.ResetToBeginning();

    }

}
