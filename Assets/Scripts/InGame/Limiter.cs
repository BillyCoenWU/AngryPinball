namespace RGSMS
{
    using UnityEngine;

    public enum SIDE
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT
    }

    public class Limiter : MonoBehaviour
    {
        [SerializeField]
        private SIDE m_side = SIDE.UP;

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

        private void Start()
        {
            Vector3 position = Vector3.zero;

            switch (m_side)
            {
                case SIDE.UP:
                    position = new Vector3(0.0f, CameraMan.Instance.maxLimitY, 0.0f);
                    break;

                case SIDE.DOWN:
                    position = new Vector3(0.0f, CameraMan.Instance.minLimitY, 0.0f);
                    break;

                case SIDE.LEFT:
                    position = new Vector3(CameraMan.Instance.minLimitX, 0.0f, 0.0f);
                    break;

                case SIDE.RIGHT:
                    position = new Vector3(CameraMan.Instance.maxLimitX, 0.0f, 0.0f);
                    break;
            }

            transform.position = position;
        }
    }
}
