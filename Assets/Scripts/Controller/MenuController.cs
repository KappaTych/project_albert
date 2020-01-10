using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string loadedScence;

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(loadedScence);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
