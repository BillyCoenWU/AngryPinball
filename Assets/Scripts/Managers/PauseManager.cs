namespace RGSMS
{
    using UnityEngine;

    public class PauseManager
    {
        private static PauseManager s_instance = null;
        public static PauseManager Instance
        {
            get
            {
                if(s_instance == null)
                {
                    s_instance = new PauseManager();
                }

                return s_instance;
            }
        }

        private bool m_pause = false;
        public bool isPaused
        {
            get
            {
                return m_pause;
            }
        }
        
        public void Pause ()
        {
            m_pause = true;
            Time.timeScale = 0.0f;
        }

        public void Resume ()
        {
            m_pause = false;
            Time.timeScale = 1.0f;
        }
    }
}
