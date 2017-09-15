namespace RGSMS
{
    namespace States
    {
        using UnityEngine;
        
        public class Normal : BallState
        {
            public Normal(Ball ball)
            {
                m_ball = ball;
            }
            
            public override void Update() { }
            public override void EnterState() { }
            public override void FixedUpdate() { }
            public override void OnCollisionEnter2D(Collision2D other) {}
        }
    }
}
