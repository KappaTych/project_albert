using System;
using UnityEngine;

public static class MovementExtensions
{
    // to an index to a slice around a circle
    /*
     * 0, 1, 2, 3, 4, 5, 6, 7
     *          N
     *          0
     *        1   7
     *    W 2       6 E
     *        3   5
     *          4
     *          S
    */ 
    public static int GetCounterClockDirection(Vector2 d)
    {
        var no_d = d.normalized;

        // 45 one circle and 8 slices
        // how many degrees one slice is
        float step = 360 / 8;
        float offset = step / 2;
        // the signed angle in degrees between A and B
        /* Dir | Angle | StepCount
         * N   | 0     | 0
         * NW  | 45    | 1
         * W   | 90    | 2
         * SW  | 135   | 3
         * S   | 180   | 4
         * SE  | -135  | 5
         * E   | -90   | 6
         * NE  | -45   | 7
         */
        float angle = Vector2.SignedAngle(Vector2.up, no_d);
        angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
