namespace RGSMS
{
    using UnityEngine;
    
    [System.Serializable]
    public class LifeManager
    {
        public LifeManager (int life)
        {
            m_maxLife = life;
            ResetLife();
        }

        private int m_life = 0;
        private int m_maxLife = 0;

        public bool isAlive
        {
            get
            {
                return (m_life > 0);
            }
        }

        public delegate void DeathEvent();
        public event DeathEvent death;

        public delegate void UpdateLife(int life);
        public event UpdateLife lifeUpdate;

        public void RemoveLife (int damage)
        {
            m_life -= damage;
            m_life = Mathf.Clamp(m_life, 0, m_maxLife);

            if(lifeUpdate != null)
            {
                lifeUpdate(m_life);
            }

            if(m_life == 0)
            {
                if(death != null)
                {
                    death();
                }
            }
        }

        public void ResetLife()
        {
            m_life = m_maxLife;

            if (lifeUpdate != null)
            {
                lifeUpdate(m_life);
            }
        }
    }
}
