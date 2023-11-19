using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPooling : MonoBehaviour
{
    [Header("풀")] public List<GameObject> m_pool;

    private GameObject m_parent;

    private void Start()
    {
        m_pool = new List<GameObject>();
        m_parent = new GameObject("Effect Parent");
        DontDestroyOnLoad(m_parent);
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject Pop(string in_name)
    {
        for(int index = 0; index < m_pool.Count; index++)
        {
            if(m_pool[index].name == in_name)
            {
                GameObject effect = m_pool[index];
                m_pool.Remove(effect);
                effect.SetActive(true);
                return effect;
            }
        }

        return null;
    }

    public void Push(GameObject in_effect)
    {
        in_effect.transform.SetParent(m_parent.transform);
        in_effect.SetActive(false);
        m_pool.Add(in_effect);
    }
}
