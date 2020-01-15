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

    public CoreEntity e;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        e = entity;
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

    public void OnAttackType(CoreEntity entity, eAttackType t)
    {
        var name = t.ToString().ToLower();
        anim.SetTrigger(name);
    }

    public void OnMove(CoreEntity entity, UnityEngine.Vector2 movement)    
    {
        if (entity.isEnableMove)
            anim.SetBool(moving, entity.move.isMoving());
    }

    public void OnDirection(CoreEntity entity, int dir)
    {
        anim.SetFloat(direction, (float)dir);
    }

    public void OnAttack(CoreEntity entity, bool acrivate)
    {
        if (acrivate)
            gameObject.GetComponent<Animator>().SetTrigger(attacking);
    }

    public void OnAttackAnimationEnd()
    {
        e.ReplaceAttack(false);
    }

    public void OnDead(CoreEntity entity)
    {
        gameObject.GetComponent<Animator>().SetTrigger(dead);
    }

    public void onDeadAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
