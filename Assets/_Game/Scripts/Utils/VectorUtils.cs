using UnityEngine;

public static class VectorUtils
{
    public static Vector2Int FloorToInt(this Vector2 vector)
    {
        return new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
    }

    public static Vector2Int FloorToVector2Int(this Vector3 vector)
    {
        return new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
    }
}
