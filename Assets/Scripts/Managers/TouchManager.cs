namespace RGSMS
{
    using UnityEngine;

    public class TouchManager
    {
        private static TouchManager s_instance = null;
        public static TouchManager Instance
        {
            get
            {
                if(s_instance == null)
                {
                    s_instance = new TouchManager();
                    Input.multiTouchEnabled = true;
                }

                return s_instance;
            }
        }

        private int m_currentTouch = 0;

        public Touch GetTouch ()
        {
            Touch t;
            int count = Input.touchCount;
            for(int i = 0; i < count; i++)
            {
                t = Input.GetTouch(i);
                if(t.phase == TouchPhase.Began)
                {
                    return t;
                }
            }

            return Input.GetTouch(0);
        }

        public void IncreaseTouchIndex()
        {
            m_currentTouch++;
        }

        public void DecreaseTouchIndex ()
        {
            m_currentTouch--;
        }
    }
}
