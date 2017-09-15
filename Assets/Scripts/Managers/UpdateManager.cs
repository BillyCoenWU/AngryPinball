namespace RGSMS
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UpdateManager : MonoBehaviour
    {
        public delegate void UpdateEvents();

        public event UpdateEvents updates;
        public event UpdateEvents fixedUpdates;

        private static UpdateManager s_instance = null;
        public static UpdateManager Instance
        {
            get
            {
                return s_instance;
            }
        }

        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.activeSceneChanged += activeSceneChanged;
            }
            else
            {
                if(s_instance != null)
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        private void Update()
        {
            if(updates != null)
            {
                updates();
            }
        }

        private void FixedUpdate()
        {
            if (fixedUpdates != null)
            {
                fixedUpdates();
            }
        }

        private void activeSceneChanged(Scene oldScene, Scene NewScene)
        {
            updates = null;
            fixedUpdates = null;
        }
    }
}
