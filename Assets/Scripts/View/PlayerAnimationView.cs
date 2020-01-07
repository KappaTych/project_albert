using UnityEngine;

public class PlayerAnimationView : MonoBehaviour, IEventListener, IMoveListener
{
    private Animator anim;
    private int lastDirection;
    private const float eps = 0.01f;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void RegisterListeners(Contexts contexts, CoreEntity entity)
    {
        entity.AddMoveListener(this);
        OnMove(entity, entity.move.movement);
    }

    public void OnMove(CoreEntity entity, UnityEngine.Vector2 movement)    
    {
        if (movement.magnitude >= eps)
            lastDirection = MovementExtensions.GetCounterClockDirection(movement);

        anim.SetFloat("dir", (float)lastDirection);
        anim.SetBool("isMove", movement.magnitude >= eps);
    }
}
