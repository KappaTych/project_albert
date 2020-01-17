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
        if (entity.hasMove)
            entity.ReplaceMove(move);
        else 
            entity.AddMove(move);

        if (Input.GetKeyDown(KeyCode.Q))
            entity.ReplaceAttack(true);

        if (Input.GetKeyDown(KeyCode.E) && 
            entity.hasMana && entity.hasManaSpeed &&
            !entity.attack.active && !entity.isFireBall)
        {
            int newMana = entity.mana.curValue - entity.playerStat.fireballManaCost;
            if (newMana >= 0)
            {
                entity.ReplaceMana(newMana);
                entity.isFireBall = true;
            } 
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

    private Vector2 _touchOrigin = -Vector2.one;

    private Vector2 GetTouchInput()
    {
        if (Input.touchCount <= 0)
            return Vector2.zero;

        int horizontal = 0, vertical = 0;
        var firstTouch = Input.touches[0];

        if (firstTouch.phase == TouchPhase.Began)
        {
            _touchOrigin = firstTouch.position;
        } 
        else if (firstTouch.phase == TouchPhase.Ended && _touchOrigin.x >= 0)
        {
            var touchEnd = firstTouch.position;
            float x = touchEnd.x - _touchOrigin.x;
            float y = touchEnd.y - _touchOrigin.y;

            // Set touchOrigin.x to -1 so that our else if statement will 
            // evaluate false and not repeat immediately.
            _touchOrigin.x = -1;

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
