namespace RGSMS
{
    using UnityEngine.SceneManagement;

    public enum SCENE
    {
        MAIN = 0,
        MAIN_MENU,
        INGAME
    }

    public class LoadSceneManager
    {
        private static readonly string[] s_sceneNames = new string[3] { "Main", "MainMenu", "InGame" };
        public static string[] sceneNames
        {
            get
            {
                return s_sceneNames;
            }
        }

        private static LoadSceneManager s_intance = null;
        public static LoadSceneManager Instance
        {
            get
            {
                if (s_intance == null)
                {
                    s_intance = new LoadSceneManager();
                }

                return s_intance;
            }
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(SCENE scene)
        {
            SceneManager.LoadScene((int)scene);
        }
    }
}
