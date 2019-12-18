using System;
using UnityEngine;

public static class MovementExtensions
{
    public static eMovement GetMovement(int x, int y)
    {
        if (x != 0)
            return x > 0 ? eMovement.Right : eMovement.Left;

        if (y != 0)
            return y > 0 ? eMovement.Up : eMovement.Down;

        throw new ArgumentException(string.Format(
            "Can't translate x:{} y{} to movement.", x, y));
    }

    public static Vector2 ToVector(this eMovement movement)
    {
        switch (movement)
        {
            case eMovement.Up:
                return new Vector2(0, 1);
            case eMovement.Right:
                return new Vector2(1, 0);
            case eMovement.Down:
                return new Vector2(0, -1);
            case eMovement.Left:
                return new Vector2(-1, 0);
            default:
                return new Vector2(0, 0);
        }
    }
}
