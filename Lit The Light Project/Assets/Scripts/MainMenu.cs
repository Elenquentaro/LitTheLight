using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        int level = SaveLoader.LoadProgress().Level;
        level = Mathf.Clamp(level, 1, SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}