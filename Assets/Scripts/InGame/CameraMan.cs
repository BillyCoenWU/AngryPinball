namespace RGSMS
{
    using Shake;
    using UnityEngine;

    public class CameraMan : Singleton<CameraMan>
    {
        private readonly Vector3 POSITION = new Vector3(0.0f, 0.0f, -10.0f);

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

        private Camera m_camera = null;
        public Camera mainCamera
        {
            get
            {
                if (m_camera == null)
                {
                    m_camera = GetComponent<Camera>();
                }

                return m_camera;
            }
        }

        private float m_maxLimitY = 0.0f;
        public float maxLimitY
        {
            get
            {
                return m_maxLimitY;
            }
        }

        private float m_minLimitY = 0.0f;
        public float minLimitY
        {
            get
            {
                return m_minLimitY;
            }
        }

        private float m_maxLimitX = 0.0f;
        public float maxLimitX
        {
            get
            {
                return m_maxLimitX;
            }
        }

        private float m_minLimitX = 0.0f;
        public float minLimitX
        {
            get
            {
                return m_minLimitX;
            }
        }

        private void Awake()
        {
            Instance = this;

            Vector3 v3 = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));
            m_maxLimitX = v3.x;
            m_maxLimitY = v3.y;
            v3 = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
            m_minLimitX = v3.x;
            m_minLimitY = v3.y;
        }

        public Vector3 MousePosition ()
        {
            return MousePosition(Input.mousePosition);
        }

        public Vector3 MousePosition(Vector3 positon)
        {
            return mainCamera.ScreenToWorldPoint(positon);
        }

        public void Shake ()
        {
            StopAllCoroutines();
            transform.position = POSITION;
            StartCoroutine(transform.ShakePositionXY(0.1f, 0.1f));
        }

       // private
    }
}
