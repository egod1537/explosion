using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public bool isTest = false;

    AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_source.isPlaying);
        _source.Play();
        if (isTest)
        {

            PlayerManager.LoadData();

            isTest = false;

        }

    }
}
