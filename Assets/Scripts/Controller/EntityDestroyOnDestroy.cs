using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntityDestroyOnDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        var e = gameObject.GetEntity<CoreEntity>();
        gameObject.Unlink();
        e.Destroy();
    }
}
