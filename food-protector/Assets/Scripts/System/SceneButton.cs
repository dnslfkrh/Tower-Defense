using UnityEngine;

public class SceneButton : MonoBehaviour
{

    public void Go(string sceneName)
    {
        CustomSceneManager.Instance.LoadScene(sceneName);
    }
}
