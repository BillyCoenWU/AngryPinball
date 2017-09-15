namespace RGSMS
{
    using UnityEngine;

    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private SCENE m_sceneToLoad = SCENE.MAIN;

        private void Start ()
        {
            LoadSceneManager.Instance.LoadScene(m_sceneToLoad);
        }
    }
}
