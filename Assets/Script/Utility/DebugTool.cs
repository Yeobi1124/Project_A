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

    public static void DrawCircle(Vector2 center, float radius, int segements = 50, Color? color = null){
        Vector2 point = Vector2.zero;
        Vector2 prev = Vector2.zero;
        Vector2 r = new Vector2(0, radius);
        float theta = 360f/segements;
        if(color == null) color = Color.green;
        
        for(int i=0;i<segements;i++){
            if(prev == Vector2.zero) prev = center + new Vector2(0, radius);
            else prev = point;
            point = center + (Vector2)(Quaternion.Euler(0, 0, theta*(i+1)) * r);
            
            Debug.DrawLine(prev, point, (Color)color);
        }
    }
}