using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dist3D
{
    public static float SphereDistance(Vector3 cam, Vector3 location, float radius) {
        return Distance(cam, location) - radius;
    }

    public static float CubeDistance(Vector3 cam, Vector3 center, Vector3 size) {
        // sqrt(max(dx, 0)^2+max(dy, 0)^2+max(dz, 0)^2) + min(max(dx, dy, dz) 0)
        Vector3 s = center + size;
        Vector3 p = center + Abs(center - cam);
        Vector3 d = new Vector3(p.x - s.x, p.y - s.y, p.z - s.z);

        return (float)(Math.Sqrt(
            Math.Pow(Math.Max(d.x, 0), 2) +
            Math.Pow(Math.Max(d.y, 0), 2) +
            Math.Pow(Math.Max(d.z, 0), 2)
        ) +
        Math.Min(Math.Max(Math.Max(d.x, d.y), d.z), 0));
    }

    public static Vector3 Max(Vector3 a, Vector3 b) {
        return new Vector3(
            Math.Max(a.x, b.x),
            Math.Max(a.y, b.y),
            Math.Max(a.z, b.z)
        );
    }

    public static Vector3 Abs(Vector3 a) {
        return new Vector3(
            Math.Abs(a.x),
            Math.Abs(a.y),
            Math.Abs(a.z)
        );
    }

    public static float Distance(Vector3 p1, Vector3 p2) {
        return (float)Math.Sqrt(
            Math.Pow(Math.Abs(p1.x-p2.x), 2) +
            Math.Pow(Math.Abs(p1.y-p2.y), 2) +
            Math.Pow(Math.Abs(p1.z-p2.z), 2)
        );
    }
}

public static class Dist2D
{
    public static float CircleDistance(Vector2 cam, Vector2 location, float radius) {
        return Distance(cam, location) - radius;
    }

    public static float RectDistance(Vector2 cam, Vector2 center, Vector2 size) {
        // sqrt(max(dx, 0)^2+max(dy, 0)^2) + min(max(dx, dy) 0)

        Vector2 s = center + size;
        Vector2 p = center + Abs(center - cam);
        Vector2 d = new Vector2(p.x - s.x, p.y - s.y);

        return (float)(Math.Sqrt(
                Math.Pow(Math.Max(d.x, 0), 2) +
                Math.Pow(Math.Max(d.y, 0), 2)
            ) +
            Math.Min(Math.Max(d.x, d.y), 0));
    }

    public static Vector2 Max(Vector2 a, Vector2 b) {
        return new Vector2(
            Math.Max(a.x, a.x),
            Math.Max(a.y, a.y)
        );
    }

    public static Vector2 Abs(Vector2 a) {
        return new Vector2(
            Math.Abs(a.x),
            Math.Abs(a.y)
        );
    }

    public static float Distance(Vector2 p1, Vector2 p2) {
        return (float)Math.Sqrt(
            Math.Pow(Math.Abs(p1.x-p2.x), 2) +
            Math.Pow(Math.Abs(p1.y-p2.y), 2)
        );
    }
}