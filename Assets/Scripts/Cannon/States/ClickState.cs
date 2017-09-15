namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public class ClickState : CannonState
        {
            private const float STATE_TIME = 2.0f;

            public ClickState(Cannon cannon)
            {
                m_cannon = cannon;
            }

            public override void EnterState()
            {
				CanvasControl.Instance.ActiveBaseInterface(m_cannon.playerId, true);
				m_cannon.animator.SetBool("PlayAnim", true);
                m_cannon.stateTime = STATE_TIME;
                m_cannon.impulseForce = 0.0f;
            }

            public override void Update()
            {
                m_cannon.impulseForce -= Time.deltaTime * 3.5f;

                if(Input.GetMouseButtonDown(0))
                {
                    m_cannon.impulseForce += 1.0f;
                }

                m_cannon.stateTime -= Time.deltaTime;
            }
        }
    }
}
