using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public string loadedScence;

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(loadedScence);
    }

    public void MainButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
