namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public class Bomb : BallState
        {
            public Bomb(Ball ball)
            {
                m_ball = ball;
            }

            public override void FixedUpdate() {}
            public override void EnterState() {}
            public override void Update() {}

            public override void OnCollisionEnter2D(Collision2D other)
            {
                Shield shield = other.gameObject.GetComponent<Shield>();

                if (shield != null)
                {
                    shield.ExplosionDamage(m_ball.data.damage);
                }
                else
                {
                    Cannon cannon = other.gameObject.GetComponent<Cannon>();

                    if (cannon != null)
                    {
                        cannon.ExplosionDamage(m_ball.data.damage);
                    }
                }
            }
        }
    }
}
