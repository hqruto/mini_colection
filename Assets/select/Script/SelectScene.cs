using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public void LoadMiniGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
