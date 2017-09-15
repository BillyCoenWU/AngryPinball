namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public class Spike : BallState
        {
            public Spike(Ball ball)
            {
                m_ball = ball;
            }

            public override void OnCollisionEnter2D(Collision2D other) { }
            public override void FixedUpdate() { }
            public override void EnterState() { }
            public override void Update() { }
        }
    }
}
