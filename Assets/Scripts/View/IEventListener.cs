using System.Collections;
using Entitas;

public interface IEventListener
{
    void RegisterListeners(Contexts contexts, CoreEntity entity);
}