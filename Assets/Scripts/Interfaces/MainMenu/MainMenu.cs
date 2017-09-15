namespace RGSMS
{
    namespace UI
    {
        using UnityEngine;

        public class MainMenu : MonoBehaviour
        {
            public void StarGame ()
            {
                LoadSceneManager.Instance.LoadScene(SCENE.INGAME);
            }
        }
    }
}
