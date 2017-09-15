namespace RGSMS
{
    using UnityEngine;
    using System.Collections.Generic;

    public class BallPool : Singleton<BallPool>
    {
        [SerializeField]
        private int MAX_BALLS = 20;
        public int maxBalls
        {
            get
            {
                return MAX_BALLS;
            }
        }

        [SerializeField]
        private Object m_ObjectBall = null;

        private Queue<Ball> m_balls = null;

        private List<Ball> m_usingBalls = null;

        private Transform m_transform = null;
        public new Transform transform
        {
            get
            {
                if(m_transform == null)
                {
                    m_transform = base.transform;
                }

                return m_transform;
            }
        }

        private void Awake()
        {
            Instance = this;
            InitPool();
            DontDestroyOnLoad(gameObject);
        }

        private void InitPool ()
        {
            m_balls = new Queue<Ball>();
            m_usingBalls = new List<Ball>();
            
            for(int  i = 0; i < MAX_BALLS; i++)
            {
                RestoreBall(CreateBall().GetComponent<Ball>());
            }
        }

        private GameObject CreateBall()
        {
            return (GameObject)Instantiate(m_ObjectBall, Vector3.zero, Quaternion.identity);
        }

        public Ball GetBall ()
        {
            Ball ball = m_balls.Count > 0 ? m_balls.Dequeue() : CreateBall().GetComponent<Ball>();

            m_usingBalls.Add(ball);

            return ball;
        }

        public void RestoreBall (Ball ball)
        {
            ball.gameObject.SetActive(false);
            m_usingBalls.Remove(ball);
            ball.transform.SetParent(transform);
            m_balls.Enqueue(ball);
        }

        public void RestoreBalls ()
        {
            int count = m_usingBalls.Count;
            for(int i = count-1; i >= 0; i--)
            {
                m_usingBalls[i].Restore();
            }
        }
    }
}
