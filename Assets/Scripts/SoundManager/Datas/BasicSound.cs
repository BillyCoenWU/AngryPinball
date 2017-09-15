namespace RGSMS
{
    namespace Sound
    {
        using UnityEngine;
        
        [CreateAssetMenu(fileName = "BasicSoundData", menuName = "RGSMS/Sound/BasicSound", order = 1)]
        public class BasicSound : SoundData
        {
            [SerializeField]
            private AudioClip m_clip = null;
            
            public override AudioClip GetClip()
            {
                return m_clip;
            }
        }
    }
}