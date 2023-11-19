using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{

    public class Save : UnityEvent { }
    public static Save OnSave = new Save();

    public int SaveDelay = 5;

    private void Start()
    {

        StartCoroutine(LoopSave());

    }

    IEnumerator LoopSave()
    {

        OnSave.Invoke();

        yield return new WaitForSeconds(SaveDelay);

        StartCoroutine(LoopSave());

    }

    private void OnDestroy()
    {

        OnSave.Invoke();

    }

    private void OnApplicationQuit()
    {

        OnSave.Invoke();

    }

}
