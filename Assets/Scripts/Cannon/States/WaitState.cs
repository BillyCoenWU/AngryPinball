namespace RGSMS
{
    namespace States
    {
        public class WaitState : CannonState
        {
            public WaitState(Cannon cannon)
            {
                m_cannon = cannon;
            }

            public override void EnterState() {}
            public override void Update() {}
        }
    }
}
