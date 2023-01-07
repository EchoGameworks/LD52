using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorChange
{

    public static Vector2 ReplaceX(this Vector2 v, float x)
    {
        v.x = x;
        return v;
    }

    public static Vector2 ReplaceY(this Vector2 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector3 ReplaceX(this Vector3 v, float x)
    {
        v.x = x;
        return v;
    }

    public static Vector3 ReplaceY(this Vector3 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector3 ReplaceZ(this Vector3 v, float z)
    {
        v.z = z;
        return v;
    }


    public static Vector3Int ReplaceX(this Vector3Int v, int x)
    {
        v.x = x;
        return v;
    }

    public static Vector3Int ReplaceY(this Vector3Int v, int y)
    {
        v.y = y;
        return v;
    }

    public static Vector3Int ReplaceZ(this Vector3Int v, int z)
    {
        v.z = z;
        return v;
    }

    public static Vector3Int ConvertToVector3Int(this Vector3 v)
    {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }

    public static Vector3Int ToVector3Int(this Vector3 v)
    {
        return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
    }

    public static Color ReplaceR(this Color c, float r)
    {
        c.r = r;
        return c;
    }

    public static Color ReplaceG(this Color c, float g)
    {
        c.g = g;
        return c;
    }

    public static Color ReplaceB(this Color c, float b)
    {
        c.b = b;
        return c;
    }

    public static Color ReplaceA(this Color c, float a)
    {
        c.a = a;
        return c;
    }

    public static Vector2 AddX(this Vector2 v, float x)
    {
        v.x += x;
        return v;
    }

    public static Vector2 AddY(this Vector2 v, float y)
    {
        v.y += y;
        return v;
    }

    public static Vector3 AddX(this Vector3 v, float x)
    {
        v.x += x;
        return v;
    }

    public static Vector3 AddY(this Vector3 v, float y)
    {
        v.y += y;
        return v;
    }

    public static Vector3 AddZ(this Vector3 v, float z)
    {
        v.z += z;
        return v;
    }

    public static Vector3Int AddX(this Vector3Int v, int x)
    {
        v.x += x;
        return v;
    }

    public static Vector3Int AddY(this Vector3Int v, int y)
    {
        v.y += y;
        return v;
    }

    public static Vector3Int AddZ(this Vector3Int v, int z)
    {
        v.z += z;
        return v;
    }


    public static Color AddR(this Color c, float r)
    {
        c.r += r;
        return c;
    }

    public static Color AddG(this Color c, float g)
    {
        c.g += g;
        return c;
    }

    public static Color AddB(this Color c, float b)
    {
        c.b += b;
        return c;
    }

    public static Color AddA(this Color c, float a)
    {
        c.a += a;
        return c;
    }

}
