using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GameJamLibrary
{

    public class SoundManager : MonoBehaviour
    {
        public const string m_resourcePath = "Sound";
        [Header("효과음 채널 수")] public uint m_channel;
        private static Dictionary<string, AudioClip> m_db;

        private static AudioSource m_background;
        private static List<AudioSource> m_effects;

        static AudioSource tr_audio;

        // Start is called before the first frame update
        void Awake()
        {

            tr_audio = GetComponent<AudioSource>();

            if (m_db == null)
            {
                DataBaseInit(m_resourcePath);
            }

            if (m_background == null)
            {
                GameObject background = new GameObject("Background Sound Channel");
                m_background = gameObject.AddComponent<AudioSource>();

                DontDestroyOnLoad(background);
            }

            if (m_effects == null)
            {
                m_effects = new List<AudioSource>();

                for (int index = 0; index < m_channel; index++)
                {
                    GameObject effect = new GameObject("Effect Sound Channel : " + (index + 1));
                    AudioSource audio = effect.AddComponent<AudioSource>();

                    DontDestroyOnLoad(effect);
                    m_effects.Add(audio);
                }
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void DataBaseInit(string in_path)
        {
            m_db = new Dictionary<string, AudioClip>();
            Object[] sounds = Resources.LoadAll(in_path, typeof(AudioClip));

            foreach (AudioClip sound in sounds)
            {
                //Debug.Log(sound.name);
                m_db.Add(sound.name, sound);
            }
        }

        /// <summary>
        /// 효과음 출력
        /// </summary>
        /// <param name="in_name">효과음 리소스 이름 : 기획안 참고</param>
        public static void EffectRun(string in_name)
        {

            try
            {
                AudioClip audio = m_db[in_name];

                bool audioFullFlag = true;

                for (int index = 0; index < m_effects.Count; index++)
                {
                    if (m_effects[index].isPlaying == false)
                    {
                        audioFullFlag = false;

                        m_effects[index].clip = audio;
                        m_effects[index].Play();
                        break;
                    }
                }

                if (audioFullFlag)
                {
                    int highValue = 0;

                    for (int index = 1; index < m_effects.Count; index++)
                    {
                        if (m_effects[highValue].time < m_effects[index].time)
                        {
                            highValue = index;
                        }
                    }

                    m_effects[highValue].clip = audio;
                    m_effects[highValue].Play();

                }
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log("효과음 사운드 없음 " + e.StackTrace);
            }
        }
        /// <summary>
        /// 배경음악 출력 : 자동 재생
        /// </summary>
        /// <param name="in_name">배경음 오브젝트 이름 : 기획안 참고 </param>
        public static void BackgroundRun(string in_name)
        {
            try
            {

                m_background.clip = m_db[in_name];

                m_background.Play();
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log("배경 사운드 없음 " + e.StackTrace);
            }
        }
    }

}
