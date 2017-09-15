namespace RGSMS
{
    namespace Sound
    {
        using UnityEngine;
        
        [CreateAssetMenu(fileName = "RandomSoundsData", menuName = "RGSMS/Sound/RandomSounds", order = 1)]
        public class RandomSounds : SoundData
        {
            [SerializeField]
            private AudioClip[] m_clips = null;
            
            public override AudioClip GetClip()
            {
                return m_clips[Random.Range(0, m_clips.Length)];
            }
        }
    }
}
