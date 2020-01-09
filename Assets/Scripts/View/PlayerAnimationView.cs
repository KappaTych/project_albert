﻿using UnityEngine;

public class PlayerAnimationView : MonoBehaviour, 
    IEventListener, IMoveListener, IDirectionListener, IAttackListener
{
    private Animator anim;

    public string direction = "dir";
    public string moving = "isMoving";
    public string attacking = "attack";

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

        OnMove(entity, entity.move.movement);
        OnDirection(entity, entity.direction.dir);
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

    public void OnAttack(CoreEntity entity)
    {
        gameObject.GetComponent<Animator>().SetTrigger("attack");
    }

    public void OnAttackEnd()
    {
        e.isAttack = false;
    }
}
