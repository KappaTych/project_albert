using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void RestartButtonPressed()
    {
        var sm = SaveManager.Instance;
        var ls = sm.LoadSave();
        var loadedScene = ls.LastLevel;
        SceneManager.LoadScene(loadedScene);
    }

    public void MenuButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
