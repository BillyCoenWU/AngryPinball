namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public class IdleState : CannonState
        {
            public IdleState(Cannon cannon)
            {
                m_cannon = cannon;
            }

            public override void EnterState() {}

            public override void Update()
            {
                if(Input.GetMouseButtonDown(0))
                {
                    m_cannon.SetState(STATE.ROTATE);
                }
            }
        }
    }
}
