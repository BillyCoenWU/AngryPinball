namespace RGSMS
{
    using Shake;
    using Sound;
    using UnityEngine;

    [System.Serializable]
    public class ShieldData
    {
        public int life = 0;
        public Sprite sprite = null;
    }
    
    public class Shield : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private Cannon m_cannon = null;
        
        private int m_currentData = 0;

        private bool m_isMoving = false;
        
        private Vector3 m_oldPosition = Vector3.zero;
        private Vector3 m_lastPosition = Vector3.zero;

        [SerializeField]
        private GameAudioObject m_audioGO = null;

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

        private SpriteRenderer m_spriteRenderer = null;
        public SpriteRenderer spriteRenderer
        {
            get
            {
                if(m_spriteRenderer == null)
                {
                    m_spriteRenderer = GetComponent<SpriteRenderer>();
                }

                return m_spriteRenderer;
            }
        }

        [SerializeField]
        private Shield[] m_otherShields = null;

        [SerializeField]
        private Collider2D[] m_colliders2D = null;

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

        #endregion

        #region Unity_Methods

        private void Start()
        {
            m_lifeIndicator.SetTarget(transform);
            SetData();
        }
        
        private void OnMouseDown()
        {
            if (MatchSettings.Instance.moment != MOMENTS.SETTINGS || MatchSettings.Instance.currentPlayer != m_cannon.playerId)
            {
                return;
            }
            
            m_isMoving = false;
            m_lastPosition = transform.position;
            m_oldPosition = CameraMan.Instance.MousePosition();
            m_oldPosition.z = 0.0f;
        }

        private void OnMouseDrag()
        {
            if (MatchSettings.Instance.moment != MOMENTS.SETTINGS || MatchSettings.Instance.currentPlayer != m_cannon.playerId)
            {
                return;
            }
            
            Vector3 v3 = CameraMan.Instance.MousePosition();
            v3.z = 0.0f;
            if (m_oldPosition != v3)
            {
                v3.x = m_cannon.playerId == 0 ? Mathf.Clamp(v3.x, CameraMan.Instance.minLimitX + 0.5f, -0.5f) : Mathf.Clamp(v3.x, 0.5f, CameraMan.Instance.maxLimitX - 0.5f);
                v3.y = Mathf.Clamp(v3.y, CameraMan.Instance.minLimitY + 0.5f, CameraMan.Instance.maxLimitY - 0.5f);

                transform.position = v3;
                m_isMoving = true;
            }
            
            m_oldPosition = transform.position;
        }

        private void OnMouseUp()
        {
            if (MatchSettings.Instance.moment != MOMENTS.SETTINGS || MatchSettings.Instance.currentPlayer != m_cannon.playerId)
            {
                return;
            }

            if (m_isMoving)
            {
                if (Vector2.Distance(m_cannon.transform.position, transform.position) < 1.0f)
                {
                    transform.position = m_lastPosition;
                }
                return;
            }

            m_currentData++;

            if (m_currentData >= MatchSettings.Instance.dataCount)
            {
                m_currentData = 0;
            }

            SetData();
        }

        #endregion

        private void Death ()
        {
            gameObject.SetActive(false);
        }

        private void SetData ()
        {
            ShieldData data = MatchSettings.Instance.GetShieldData(m_currentData);

            spriteRenderer.sprite = data.sprite;

            int count = m_colliders2D.Length;
            for(int  i = 0; i < count; i++)
            {
                m_colliders2D[i].enabled = (i == m_currentData);
            }
            
            m_lifeManager = new LifeManager(data.life);
            m_lifeManager.death += Death;
            m_lifeManager.lifeUpdate += m_lifeIndicator.SetLife;
            m_lifeManager.ResetLife();
        }
        
        public void PlayOnCollision ()
        {
            m_audioGO.Play();
        }

        public void ExplosionDamage (int damage)
        {
            int count = m_otherShields.Length;

            for(int i = 0; i < count; i++)
            {
                if(Mathf.Abs(Vector2.Distance(transform.position, m_otherShields[i].transform.position)) < 2.0f)
                {
                    m_otherShields[i].lifeManager.RemoveLife(damage);
                }
            }
        }
    }
}
