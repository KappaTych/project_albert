using UnityEngine;

public class PlayerAnimationView : MonoBehaviour, 
    IEventListener, IMoveListener, IDirectionListener
{
    private Animator anim;

    public string direction = "dir";
    public string moving = "isMoving";
    
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddMoveListener(this);
        OnMove(entity, entity.move.movement);
        entity.AddDirectionListener(this);
        OnDirection(entity, entity.direction.dir);
    }

    public void OnMove(CoreEntity entity, UnityEngine.Vector2 movement)    
    {
        anim.SetBool(moving, entity.move.isMoving());
    }

    public void OnDirection(CoreEntity entity, int dir)
    {
        anim.SetFloat(direction, (float)dir);
    }
}
