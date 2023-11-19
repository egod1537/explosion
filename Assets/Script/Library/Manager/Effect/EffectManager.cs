using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameJamLibrary
{

    public class EffectManager : MonoBehaviour
    {
        public const string m_reourcesPath = "Effect";
        public static EffectPooling m_pool;
        private static Dictionary<string/* effect Name*/, GameObject> m_db;

        private void Start()
        {
            if (m_db == null)
            {
                DataBaseInit(m_reourcesPath);
            }

            if (m_pool == null)
            {
                m_pool = FindObjectOfType<EffectPooling>();
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void DataBaseInit(string in_path)
        {
            m_db = new Dictionary<string, GameObject>();
            Object[] objects = Resources.LoadAll(in_path);

            if (objects == null) Debug.Log(in_path);

            foreach (GameObject obj in objects)
            {
                m_db.Add(obj.name, obj);
            }
        }

        /// <summary>
        /// 이팩트 생성
        /// </summary>
        /// <param name="in_pos">인게임 내부의 생성되야하는 위치 </param>
        /// <param name="in_name">프리펩 이름 : 기획안 참고</param>
        public static void Create(Vector3 in_pos, string in_name)
        {

            in_name = m_reourcesPath + "/" + in_name;

            try
            {
                GameObject child = null;

                if (m_pool.m_pool.Count == 0)
                {
                    GameObject prefeb = m_db[in_name];
                    child = Instantiate(prefeb);
                    child.name = prefeb.name;
                }
                else
                {
                    child = m_pool.Pop(in_name);

                    if (child == null)
                    {
                        GameObject prefeb = m_db[in_name];
                        child = Instantiate(prefeb);
                        child.name = prefeb.name;
                    }
                }

                child.transform.position = in_pos;

            }
            catch (KeyNotFoundException e)
            {
                Debug.Log(e.StackTrace);
            }
        }
    }

}
