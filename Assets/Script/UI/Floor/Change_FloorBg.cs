using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_FloorBg : MonoBehaviour
{

    private void Start()
    {

        StartCoroutine(AllChangeBg(StageManager.stage));

        StageManager.OnNextStage.AddListener((StageManager.Stage stage) =>
        {

            StartCoroutine(AllChangeBg(stage));

        });

    }

    public IEnumerator AllChangeBg(StageManager.Stage stage)
    {

        yield return new WaitForSeconds(0.75f);

        int count = transform.childCount;

        for (int i = 0; i < count; i++)
            transform.GetChild(i).GetComponent<UISprite>().spriteName
                = ((int)stage + 1).ToString();

    }

}
