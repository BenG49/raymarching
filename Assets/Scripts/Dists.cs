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

        return (float)(Mathf.Sqrt(
            Mathf.Pow(Mathf.Max(d.x, 0), 2) +
            Mathf.Pow(Mathf.Max(d.y, 0), 2) +
            Mathf.Pow(Mathf.Max(d.z, 0), 2)
        ) +
        Mathf.Min(Mathf.Max(Mathf.Max(d.x, d.y), d.z), 0));
    }

    public static Vector3 Max(Vector3 a, Vector3 b) {
        return new Vector3(
            Mathf.Max(a.x, b.x),
            Mathf.Max(a.y, b.y),
            Mathf.Max(a.z, b.z)
        );
    }

    public static Vector3 Abs(Vector3 a) {
        return new Vector3(
            Mathf.Abs(a.x),
            Mathf.Abs(a.y),
            Mathf.Abs(a.z)
        );
    }

    public static float Distance(Vector3 p1, Vector3 p2) {
        return (float)Mathf.Sqrt(
            Mathf.Pow(Mathf.Abs(p1.x-p2.x), 2) +
            Mathf.Pow(Mathf.Abs(p1.y-p2.y), 2) +
            Mathf.Pow(Mathf.Abs(p1.z-p2.z), 2)
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

        return (float)(Mathf.Sqrt(
                Mathf.Pow(Mathf.Max(d.x, 0), 2) +
                Mathf.Pow(Mathf.Max(d.y, 0), 2)
            ) +
            Mathf.Min(Mathf.Max(d.x, d.y), 0));
    }

    public static Vector2 Max(Vector2 a, Vector2 b) {
        return new Vector2(
            Mathf.Max(a.x, a.x),
            Mathf.Max(a.y, a.y)
        );
    }

    public static Vector2 Abs(Vector2 a) {
        return new Vector2(
            Mathf.Abs(a.x),
            Mathf.Abs(a.y)
        );
    }

    public static float Distance(Vector2 p1, Vector2 p2) {
        return (float)Mathf.Sqrt(
            Mathf.Pow(Mathf.Abs(p1.x-p2.x), 2) +
            Mathf.Pow(Mathf.Abs(p1.y-p2.y), 2)
        );
    }
}

public static class Rotation
{
    public static Vector3 Rot3D(Vector3 angle, Vector3 initial) {
        Vector3 r = angle * (Mathf.PI/180);

        float[] sin = new float[] {Mathf.Sin(r.x), Mathf.Sin(r.y), Mathf.Sin(r.z)};
        float[] cos = new float[] {Mathf.Cos(r.x), Mathf.Cos(r.y), Mathf.Cos(r.z)};

        Matrix yaw = new Matrix(new float[,]{
            {cos[0], -sin[0], 0},
            {sin[0], cos[0],  0},
            {0,      0,       1}
        });
        Matrix pitch = new Matrix(new float[,]{
            {cos[1],  0, sin[1]},
            {0,       1, 0},
            {-sin[1], 0, cos[1]}
        });
        Matrix roll = new Matrix(new float[,]{
            {1, 0,      0},
            {0, cos[2], -sin[2]},
            {0, sin[2], cos[2]}
        });

        Matrix product = yaw*(pitch*(roll*initial));
        
        return new Vector3(product[0,0], product[0,1], product[0,2]);
    }

    public static Vector2 Rot2D(float angle, Vector2 initial) {
        float rad = angle * (Mathf.PI/180);

        Matrix rot = new Matrix(new float[,]{
            {Mathf.Cos(rad), -Mathf.Sin(rad)},
            {Mathf.Sin(rad), Mathf.Cos(rad)}
        });

        Matrix product = rot*initial;

        return new Vector2(product[0,0], product[0,1]);
    }
}