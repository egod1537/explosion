using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLibrary : MonoBehaviour
{

    public TweenAlpha Blind;

    static bool isLoad = false;

    static string sceneName;

    float time;
    bool isAni = false;

    private void Awake()
    {

        DontDestroyOnLoad(Blind.transform.parent.gameObject);

    }

    private void Update()
    {

        if (isLoad)
        {

            Blind.from = 0;
            Blind.to = 1;

            Blind.enabled = true;
            Blind.ResetToBeginning();
            time = 0.5f;

            isAni = true;

            isLoad = false;

        }

        if (isAni)
        {

            if (time <= 0)
            {

                SceneManager.LoadScene(sceneName);

                Blind.from = 1;
                Blind.to = 0;

                Blind.enabled = true;
                Blind.ResetToBeginning();

                isAni = false;

            }

        }

        if (time > 0) time -= Time.deltaTime;

    }

    public static void Load(string _sceneName)
    {

        isLoad = true;
        sceneName = _sceneName;

    }

}
