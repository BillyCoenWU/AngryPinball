namespace RGSMS
{
    namespace States
    {
        using UnityEngine;

        public class RotateState : CannonState
        {
            private Vector2 m_angle = Vector2.zero;

            public RotateState(Cannon cannon)
            {
                m_cannon = cannon;
            }

            public override void EnterState() 
			{
				m_cannon.particle.Play (false);
			}

            public override void Update()
            {
                if(Input.GetMouseButtonUp(0))
				{
					m_cannon.particle.Stop (false);
					m_cannon.particle.Clear ();
                    m_cannon.SetState(STATE.CLICKS);
                    return;
                }

                m_angle = CameraMan.Instance.MousePosition() - m_cannon.transform.position;
                m_cannon.transform.up = m_angle;
				m_cannon.body.rotation = Quaternion.Inverse(m_cannon.transform.rotation);
            }
        }
    }
}
