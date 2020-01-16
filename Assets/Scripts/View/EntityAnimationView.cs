using UnityEngine;

public class EntityAnimationView : MonoBehaviour, 
    IEventListener, IMoveListener, IDirectionListener, 
    IAttackListener, IDeadListener, IAttackTypeListener
{
    private Animator anim;

    public string direction = "dir";
    public string moving = "isMoving";
    public string attacking = "attack";
    public string dead = "dead";

    private int entityId;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        if (entity == null)
            return;

        entityId = entity.entityId.id;
        entity.AddMoveListener(this);
        entity.AddDirectionListener(this);
        entity.AddAttackListener(this);
        entity.AddDeadListener(this);
        entity.AddAttackTypeListener(this);

        if (entity.hasAttackType)
            OnAttackType(entity, entity.attackType.t);
        OnMove(entity, entity.move.movement);
        OnDirection(entity, entity.direction.dir);
    }
    
    public void UnregisterListeners(Contexts contexts, CoreEntity entity)
    {
        if (entity == null)
            return;

        OnMove(entity, Vector2.zero);

        entity.RemoveMoveListener(this);
        entity.RemoveDirectionListener(this);
        entity.RemoveAttackListener(this);
        entity.RemoveDeadListener(this);
        entity.RemoveAttackTypeListener(this);
    }

    public void OnAttackType(CoreEntity _, eAttackType t)
    {
        var name = t.ToString().ToLower();
        anim?.SetTrigger(name);
    }

    public void OnMove(CoreEntity entity, UnityEngine.Vector2 _)    
    {
        if (entity != null && entity.isEnableMove)
            anim?.SetBool(moving, entity.move.isMoving());
    }

    public void OnDirection(CoreEntity _, int dir)
    {
        anim?.SetFloat(direction, (float)dir);
    }

    public void OnAttack(CoreEntity _, bool acrivate)
    {
        if (acrivate)
            gameObject.GetComponent<Animator>()?.SetTrigger(attacking);
    }

    public void OnAttackAnimationEnd()
    {
        var e = Contexts.sharedInstance.core.GetEntityWithEntityId(entityId);
        if (e == null)
            return;
        
        e.ReplaceAttack(false);
    }

    public void OnDead(CoreEntity entity)
    {
        gameObject.GetComponent<Animator>()?.SetTrigger(dead);
    }

    public void onDeadAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
