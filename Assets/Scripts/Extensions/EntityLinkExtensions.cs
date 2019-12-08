using UnityEngine;
using Entitas.Unity;

public static class EntityLinkExtensions
{
    public static T GetEntity<T>(this GameObject g) where T : IEntityIdEntity
    {
        var link = g.GetEntityLink();
        return (T) link?.entity;
    }
}
