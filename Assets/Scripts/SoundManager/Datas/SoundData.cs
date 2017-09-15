namespace RGSMS
{
    namespace Sound
    {
        using UnityEngine;

        public abstract class SoundData : ScriptableObject
        {
            [SerializeField, Range(0.0f, 1.0f)]
            private float m_volume = 1.0f;
            public float volume
            {
                get
                {
                    return m_volume;
                }
            }

            [SerializeField, Range(0.1f, 1.0f)]
            public float m_pitch = 1.0f;
            public float pitch
            {
                get
                {
                    return m_pitch;
                }
            }

            protected AudioSource m_source = null;
            public AudioSource audioSource
            {
                set
                {
                    m_source = value;
                }
            }
            
            public abstract AudioClip GetClip();
        }    
    }
}
