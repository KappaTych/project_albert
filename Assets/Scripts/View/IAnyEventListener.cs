using System.Collections;
using Entitas;

public interface IAnyEventListener
{
    void RegisterListeners(Contexts contexts);
    void UnregisterListeners(Contexts contexts);
}