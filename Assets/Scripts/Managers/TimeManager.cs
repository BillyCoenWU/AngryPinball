namespace RGSMS
{
    using UnityEngine;

    public class TimeManager
    {
        private static TimeManager s_instance = null;
        public static TimeManager Instance
        {
            get
            {
                if(s_instance == null)
                {
                    s_instance = new TimeManager();
                }

                return s_instance;
            }
        }

        private const string FORMAT = "s";

        private const float MAX_TIME = 10.0f;
        private float m_time = 0.0f;

        public string time
        {
            get
            {
                return string.Format(Mathf.FloorToInt(m_time % 60.0f).ToString(), FORMAT);
            }
        }
        
        public bool IsDone
        {
            get
            {
                return Mathf.Approximately(m_time, 0.0f);
            }
        }

        public void UpdateTime ()
        {
            m_time -= Time.deltaTime;
            m_time = Mathf.Clamp(m_time, 0.0f, MAX_TIME);
        }

        public void Reset ()
        {
            m_time = MAX_TIME;
        }
    }
}
