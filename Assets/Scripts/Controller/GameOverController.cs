using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour, IAnyEventListener, IAnyPlayerDeadListener
{
    public int entityId;
    public bool isRegister = false;

    void Start()
    {
        if (!isRegister)
            RegisterListeners(Contexts.sharedInstance);
    }

    private void OnDestroy()
    {
        if (!isRegister)
            UnregisterListeners(Contexts.sharedInstance);
    }
 
    public void RegisterListeners(Contexts contexts)
    {
        var e = contexts.core.CreateEntity();
        entityId = e.entityId.id;
        isRegister = true;
        e.AddAnyPlayerDeadListener(this);
    }
    public void UnregisterListeners(Contexts contexts)
    {
        var e = contexts.core.GetEntityWithEntityId(entityId);
        isRegister = false;
        if (e == null)
            return;
        e.RemoveAnyPlayerDeadListener(this, false);
        e.isDestroyEntity = true;
    }

    public void OnAnyPlayerDead(CoreEntity entity)
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        Destroy(target);
        SceneManager.LoadScene("DeathScreen");
    }

}
