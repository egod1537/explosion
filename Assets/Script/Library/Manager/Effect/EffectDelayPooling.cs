using System.Collections;
using System.Collections.Generic;
using GameJamLibrary;
using UnityEngine;

public class EffectDelayPooling : MonoBehaviour
{
    [Header("필드위에 있는 지속시간")] public float m_delay;
    private float m_current;
   
    // Update is called once per frame
    void Update()
    {
        m_current += Time.deltaTime;

        if(m_delay <= m_current)
        {
            EffectManager.m_pool.Push(this.gameObject);
        }
    }
}
