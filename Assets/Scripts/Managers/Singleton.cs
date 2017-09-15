using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance = null;

    public static T Instance
    {
        get
		{
			return s_instance;
		}

        set
		{
			s_instance = value;
		}
    }
}
