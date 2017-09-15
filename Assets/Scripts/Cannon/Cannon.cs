namespace RGSMS
{
    using Sound;
    using States;
    using UnityEngine;
    using System.Collections.Generic;

    public class Cannon : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private int m_playerId = 0;
        public int playerId
        {
            get
            {
                return m_playerId;
            }
        }
        
        [SerializeField]
        private int m_life = 0;
        private int m_deadBalls = 0;
        private int m_ballsCount = 0;
        private int m_currentBalls = 0;

        [SerializeField]
        private float m_distance = 1.0f;

        private float m_stateTime = 0.0f;
        public float stateTime
        {
            get
            {
                return m_stateTime;
            }

            set
            {
                m_stateTime = value;
                m_stateTime = Mathf.Clamp(m_stateTime, 0.0f, 2.0f);

                CanvasControl.Instance.UpdateTimeBar(m_playerId, Mathf.InverseLerp(0.0f, 2.0f, m_stateTime));

                if (Mathf.Approximately(m_stateTime, 0.0f))
                {
                    LaunchBalls();
					SetState(STATE.WAIT);
					m_animator.SetBool("PlayAnim", false);
					CanvasControl.Instance.UpdatePowerBar(m_playerId, 0.0f);
					CanvasControl.Instance.ActiveBaseInterface(m_playerId, false);
                }
            }
        }

        private float m_impulseForce = 0.0f;
        public float impulseForce
        {
            get
            {
                return m_impulseForce;
            }

            set
            {
                m_impulseForce = value;
                m_impulseForce = Mathf.Clamp(m_impulseForce, 0.0f, 10.0f);

                CanvasControl.Instance.UpdatePowerBar(m_playerId, Mathf.InverseLerp(0.0f, 10.0f, m_impulseForce));
            }
        }

		[SerializeField]
		private Animator m_animator = null;
		public Animator animator
		{
			get
			{
				return m_animator;
			}
		}

        [SerializeField]
        private LifeIndicator m_lifeIndicator = null;

        private LifeManager m_lifeManager = null;
        public LifeManager lifeManager
        {
            get
            {
                return m_lifeManager;
            }
		}

        [SerializeField]
        private GameAudioObject m_audio = null;

        [SerializeField]
		private Transform m_launchPoint = null;

		[SerializeField]
		private Transform m_body = null;
        public Transform body
        {
            get
            {
                return m_body;
            }
        }
        
        [SerializeField]
		private ParticleSystem m_particle;
        public ParticleSystem particle
        {
            get
            {
                return m_particle;
            }
        }

        [SerializeField]
        private Shield[] m_shields = null;

        private Transform m_transform = null;
        public new Transform transform
        {
            get
            {
                if (m_transform == null)
                {
                    m_transform = base.transform;
                }

                return m_transform;
            }
        }
        
        private Queue<BallData> m_ballsData = null;

        private CannonState m_currentState = null;
        private Dictionary<STATE, CannonState> m_states = null;

        private const string LAUNCH_BALL = "LaunchBall";

        #endregion

        private void Start ()
        {
            SetInitialPosition();
            m_lifeIndicator.SetTarget(transform);

            m_ballsData = new Queue<BallData>();

            m_lifeManager = new LifeManager(m_life);
            m_lifeManager.lifeUpdate += m_lifeIndicator.SetLife;
            m_lifeManager.death += Death;
            m_lifeManager.ResetLife();

            m_states = new Dictionary<STATE, CannonState>();
            m_states.Add(STATE.IDLE, new IdleState(this));
            m_states.Add(STATE.WAIT, new WaitState(this));
            m_states.Add(STATE.CLICKS, new ClickState(this));
            m_states.Add(STATE.ROTATE, new RotateState(this));

            SetState(STATE.WAIT);
        }

        private void Death ()
        {
            if(MatchSettings.Instance.moment == MOMENTS.END_MATCH)
            {
                return;
            }
            
            gameObject.SetActive(false);

            int winner = m_playerId == 0 ? 1 : 0;

            MatchSettings.Instance.Win(winner);
        }

        private void LaunchBall ()
        {
            m_currentBalls--;
            CanvasControl.Instance.SetPlayerBallCount(m_playerId, m_currentBalls);

            Ball ball = BallPool.Instance.GetBall();
            ball.transform.SetParent(null);
            
            Vector3 up = transform.up;
            up += (Random.insideUnitSphere * 0.05f);
            up.z = 0.0f;

            ball.Launch(this, m_ballsData.Dequeue(), m_launchPoint.position, up, m_impulseForce);
        }

        private void LaunchBalls ()
        {
            m_deadBalls = 0;

            m_currentBalls = m_ballsData.Count;
            int count = m_currentBalls;
            for (int i = 0; i < count; i++)
            {
                Invoke(LAUNCH_BALL, 0.1f * i);
            }
        }

        private void CustomUpdate ()
        {
            if (m_currentState == null || MatchSettings.Instance.moment == MOMENTS.END_MATCH)
            {
                return;
            }

            m_currentState.Update();
        }

        private void SetInitialPosition ()
        {
            switch(m_playerId)
            {
                case 0:
                    transform.position = new Vector3(CameraMan.Instance.minLimitX + m_distance, 0.0f, 0.0f);
                    break;

                case 1:
                    transform.position = new Vector3(CameraMan.Instance.maxLimitX - m_distance, 0.0f, 0.0f);
                    break;
            }
        }

        public void UpdateBalls ()
        {
            m_ballsCount++;

            for (int i = 0; i < m_ballsCount; i++)
            {
                m_ballsData.Enqueue(MatchSettings.Instance.GetRandomBallData());
            }
        }

        public void IncludeUpdate ()
        {
            UpdateManager.Instance.updates += CustomUpdate;
        }

        public void ExcludeUpdate ()
        {
            UpdateManager.Instance.updates -= CustomUpdate;
        }

        public void UpdateDeadBalls ()
        {
            m_deadBalls++;

            if (m_deadBalls >= m_ballsCount)
            {
                MatchSettings.Instance.EndPlayerTurn();
            }
        }

        public void SetState (STATE state)
        {
            m_currentState = m_states[state];

            if(m_currentState != null)
            {
                m_currentState.EnterState();
            }
        }

        public void PlaySound ()
        {
            m_audio.Play();
        }

        public void ExplosionDamage (int damage)
        {
            int count = m_shields.Length;
            for(int i = 0; i < count; i++)
            {
                if (Mathf.Abs(Vector2.Distance(transform.position, m_shields[i].transform.position)) < 2.0f)
                {
                    m_shields[i].lifeManager.RemoveLife(damage);
                }
            }
        }
    }
}
