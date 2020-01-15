using UnityEngine;
using Entitas.Unity;

public static class EntityLinkExtensions
{
    public static T GetEntity<T>(this GameObject g) where T : IEntityIdEntity
    {
        return (T)g.GetEntityLink()?.entity;
    }
}
