using UnityEngine;

public class PlayerAnimationView : MonoBehaviour, IEventListener, IMoveListener
{
    private Animator anim;
    private int lastDirection;
    private const float eps = 0.01f;

    public string[] staticDirection =
    {
        "static N", "static NW", "static W",
        "static SW", "static S", "static SE",
        "static E", "static NE",
    };

    public string[] runDirections =
    {
        "run N", "rum NW", "run W",
        "run SW", "run S", "run SE",
        "run E", "run NE",
    };

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
        var directionArray = movement.magnitude < eps ? staticDirection : runDirections;
        // if run
        if (movement.magnitude >= eps)
            lastDirection = MovementExtensions.GetCounterClockDirection(movement);

        anim.Play(directionArray[lastDirection]);
    }
}
