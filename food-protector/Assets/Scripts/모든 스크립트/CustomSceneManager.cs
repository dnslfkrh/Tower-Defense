using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    private static CustomSceneManager instance;

    public static CustomSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObj = new GameObject("SceneController");
                instance = singletonObj.AddComponent<CustomSceneManager>();
                DontDestroyOnLoad(singletonObj);
            }
            return instance;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
