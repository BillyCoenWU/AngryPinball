namespace RGSMS
{
    using States;
    using UnityEngine;

    [System.Serializable]
    public class BallData
    {
        public TYPE type = TYPE.NORMAL;

        public int damage = 0;

        public float speed = 0.0f;
        
        public Sprite sprite = null;
    }
    
    public class Ball : MonoBehaviour
    {
        private int m_impacts = 0;

        private float m_launchSpeed = 0.0f;

        private Cannon m_cannon = null;

        private BallData m_data = null;
        public BallData data
        {
            get
            {
                return m_data;
            }
        }

        private BallState m_state = null;

        private bool m_crossLimit = false;

        private Vector2 m_lastVelocity = Vector2.zero;

        private Rigidbody2D m_rigidbody2D = null;
        public Rigidbody2D _rigidbody2D
        {
            get
            {
                if(m_rigidbody2D == null)
                {
                    m_rigidbody2D = GetComponent<Rigidbody2D>();
                }

                return m_rigidbody2D;
            }
        }
        
        private void CustomUpdate()
        {
            if(m_state != null)
            {
                m_state.Update();
            }
            
            if(m_crossLimit)
            {
                if (m_cannon.playerId == 0)
                {
                    if (transform.position.x < 0.0f)
                    {
                        Death();
                    }
                }
                else
                {
                    if (transform.position.x > 0.0f)
                    {
                        Death();
                    }
                }
            }
            else
            {
                if(m_cannon.playerId == 0)
                {
                    if(transform.position.x > 0.0f)
                    {
                        m_crossLimit = true;
                    }
                }
                else
                {
                    if (transform.position.x < 0.0f)
                    {
                        m_crossLimit = true;
                    }
                }
			}

            if (_rigidbody2D.velocity.magnitude < 5.0f && gameObject.activeSelf)
            {
                Death();
            }

			m_lastVelocity = m_rigidbody2D.velocity;
        }
        
        private void Death ()
        {
            m_cannon.UpdateDeadBalls();
            Restore();
        }

        public void Restore ()
        {
            UpdateManager.Instance.updates -= CustomUpdate;
            BallPool.Instance.RestoreBall(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(m_state != null)
            {
                m_state.OnCollisionEnter2D(other);
            }
            
            m_rigidbody2D.velocity = Vector2.Reflect(m_lastVelocity.normalized, other.contacts[0].normal) * m_launchSpeed;
            
            m_lastVelocity = m_rigidbody2D.velocity;

            Shield shield = other.gameObject.GetComponent<Shield>();

            if(shield != null)
            {
                m_impacts++;
                
                if (MatchSettings.Instance.moment != MOMENTS.END_MATCH)
                {
                    CameraMan.Instance.Shake();
                    shield.PlayOnCollision();
                    shield.lifeManager.RemoveLife(m_data.damage);
                }
            }
            else
            {
                Cannon cannon = other.gameObject.GetComponent<Cannon>();

                if(cannon != null)
                {
                    m_impacts++;

                    if (MatchSettings.Instance.moment != MOMENTS.END_MATCH)
                    {
                        CameraMan.Instance.Shake();
                        cannon.PlaySound();
                        cannon.lifeManager.RemoveLife(m_data.damage);
                    }
                }
            }
            
            if(m_impacts >= 5)
            {
                Death();
            }
        }

        public void SetType()
        {
            switch(m_data.type)
            {
                case TYPE.BOMB:
                    m_state = new Bomb(this);
                    break;

                case TYPE.SPIKE:
                    m_state = new Spike(this);
                    break;

                case TYPE.MULTI:
                    m_state = new Multi(this);
                    break;

                default:
                    m_state = new Normal(this);
                    break;
            }
        }

        public void Launch (Cannon cannon, BallData data, Vector3 position, Vector3 up, float velocity)
        {
            m_impacts = 0;
            m_crossLimit = false;

            gameObject.SetActive(true);
            gameObject.layer = cannon.gameObject.layer;

            m_cannon = cannon;

            m_data = data;
            SetType();

            transform.up = up;
            transform.position = position;

            m_launchSpeed = velocity * m_data.speed;

            _rigidbody2D.velocity = up * m_launchSpeed;

            /*
            if(_rigidbody2D.velocity.magnitude < 10.0f)
            {
                _rigidbody2D.velocity *= 7.5f;
            }
            */

            m_lastVelocity = _rigidbody2D.velocity;

            UpdateManager.Instance.updates += CustomUpdate;
        }
    }
}
