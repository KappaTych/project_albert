using UnityEngine;
using Entitas;
using Entitas.Unity;

public class InputController : MonoBehaviour
{

    void Update()
    {
        var entity = gameObject.GetEntity<CoreEntity>();
        if (entity == null)
            return;

        var move = GetInput();
        int horizontal = Mathf.RoundToInt(move.x);
        int vertical = Mathf.RoundToInt(move.y);

        if (horizontal != 0 || vertical != 0)
        {
            var movement = MovementExtensions.GetMovement(horizontal, vertical);
            entity.ReplaceInput(movement);
        }
    }

    private Vector2 GetInput()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        return GetTouchInput();
#else
        return GetAxisInput();
#endif
    }

    private Vector2 GetAxisInput()
    {
        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private Vector2 touchOrigin = -Vector2.one;

    private Vector2 GetTouchInput()
    {
        if (Input.touchCount <= 0)
            return Vector2.zero;

        int horizontal = 0, vertical = 0;
        var firstTouch = Input.touches[0];

        if (firstTouch.phase == TouchPhase.Began)
        {
            touchOrigin = firstTouch.position;
        } 
        else if (firstTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
        {
            var touchEnd = firstTouch.position;
            float x = touchEnd.x - touchOrigin.x;
            float y = touchEnd.y - touchOrigin.y;

            // Set touchOrigin.x to -1 so that our else if statement will 
            // evaluate false and not repeat immediately.
            touchOrigin.x = -1;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                horizontal = x > 0 ? 1 : -1;
            }
            else
            {
                vertical = y > 0 ? 1 : -1;
            }


        }

        return new Vector2(horizontal, vertical);
    }
}
