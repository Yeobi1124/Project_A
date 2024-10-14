using UnityEngine;

public class DebugTool
{
    public static void DrawRectangle(Vector2 pos, Vector2 value, Color? color = null){
        Vector2 vecHorizion = new Vector2(value.x, 0);
        Vector2 vecVertical = new Vector2(0, value.y);
        color = color ?? Color.green;

        Debug.DrawLine(pos - vecHorizion/2 - vecVertical/2, pos - vecHorizion/2 + vecVertical/2, (Color)color);
        Debug.DrawLine(pos - vecHorizion/2 - vecVertical/2, pos + vecHorizion/2 - vecVertical/2, (Color)color);
        Debug.DrawLine(pos + vecHorizion/2 + vecVertical/2, pos - vecHorizion/2 + vecVertical/2, (Color)color);
        Debug.DrawLine(pos + vecHorizion/2 + vecVertical/2, pos + vecHorizion/2 - vecVertical/2, (Color)color);
    }
}