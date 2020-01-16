using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour, IAnyEventListener, IAnyPlayerDeadListener
{
    void Awake()
    {
        RegisterListeners(Contexts.sharedInstance);
    }

    public void RegisterListeners(Contexts contexts)
    {
        contexts.core.CreateEntity().AddAnyPlayerDeadListener(this);
    }

    public void OnAnyPlayerDead(CoreEntity entity)
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        Destroy(target);
        SceneManager.LoadScene("DeathScreen");
    }
}
