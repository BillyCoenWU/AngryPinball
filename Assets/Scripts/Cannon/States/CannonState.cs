namespace RGSMS
{
    namespace States
    {
        public enum STATE
        {
            NONE = 0,
            IDLE,
            ROTATE,
            CLICKS,
            WAIT
        }

        public abstract class CannonState
        {
            protected Cannon m_cannon = null;

            public abstract void EnterState();
            public abstract void Update();
        }
    }
}
