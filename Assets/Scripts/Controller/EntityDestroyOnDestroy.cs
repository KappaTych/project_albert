using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EntityDestroyOnDestroy : MonoBehaviour
{
    private CoreEntity e;
    private void Start()
    {
        e = gameObject.GetEntity<CoreEntity>();
    }

    void OnDestroy()
    {
        e?.RemoveGameObject();
        gameObject.Unlink();
    }
}
