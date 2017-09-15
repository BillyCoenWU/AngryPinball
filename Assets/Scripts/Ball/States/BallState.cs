namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public enum TYPE
        {
            NORMAL = 0,
            BOMB,
            SPIKE,
            MULTI
        }

        public abstract class BallState
        {
            protected Ball m_ball = null;

            public abstract void OnCollisionEnter2D(Collision2D other);
            public abstract void FixedUpdate();
            public abstract void EnterState();
            public abstract void Update();
        }
    }
}
