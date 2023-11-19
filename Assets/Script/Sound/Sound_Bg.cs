using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;

public class Sound_Bg : MonoBehaviour
{

    private void Awake()
    {

        StageManager.OnNextStage.AddListener((StageManager.Stage stage) =>
        {

            SoundManager.BackgroundRun("bg_" + ((int)stage+1).ToString());

        });

    }

    private void Start()
    {

        SoundManager.BackgroundRun("bg_" + ((int)StageManager.stage+1).ToString());

    }

}
