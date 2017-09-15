namespace RGSMS
{
    namespace Sound
    {
        using UnityEngine;
        using System.Collections.Generic;

        public class SoundManager : MonoBehaviour
        {
            [SerializeField]
            private SoundData[] m_bgms = null;

            [SerializeField]
            private SoundData[] m_sfxs = null;

            private List<GameAudioObject> m_gaos = new List<GameAudioObject>();
            public List<GameAudioObject> gameAudioLists
            {
                get
                {
                    return m_gaos;
                }
            }

            private static SoundManager s_instance = null;
            public static SoundManager Instance
            {
                get
                {
                    return s_instance;
                }
            }

            private void Awake()
            {
                if(s_instance == null)
                {
                    s_instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    if(s_instance != this)
                    {
                        DestroyImmediate(gameObject);
                    }
                }
            }

            public SoundData GetSFXByIndex (int index)
            {
                return m_sfxs[index];
            }

            public SoundData GetBGMByIndex(int index)
            {
                return m_bgms[index];
            }

            public void Stop()
            {
                int count = m_gaos.Count;
                for(int  i = 0; i < count; i++)
                {
                    m_gaos[i].Stop();
                }
            }

            public void Pause()
            {
                int count = m_gaos.Count;
                for (int i = 0; i < count; i++)
                {
                    m_gaos[i].Pause();
                }
            }

            public void UnPause()
            {
                int count = m_gaos.Count;
                for (int i = 0; i < count; i++)
                {
                    m_gaos[i].UnPause();
                }
            }
        }
    }
}
