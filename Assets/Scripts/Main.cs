using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private bool THREE_DIM = true;
    [SerializeField]
    public int angle
    {
        get {return m_angle; }
        set {m_angle = value; }
    }
    [Range(0, 360)]
    public int m_angle;

    [SerializeField]
    public int angle2
    {
        get {return m_angle2; }
        set {m_angle2 = value; }
    }
    [Range(-90, 90)]
    public int m_angle2;

    private RayMarch2D main2;
    private RayMarch3D main3;

    void Start()
    {
        if (!THREE_DIM) {
            main2 = new RayMarch2D(
                new Vector2(0, 0),
                new RayMarch2D.Shape[] {
                    new RayMarch2D.Shape(
                        RayMarch2D.ShapeType.Rectangle,
                        new Vector2(0, 2),
                        new Vector2(1, 1),
                        RayMarch2D.ShapeEnv.Scene
                    ),
                    new RayMarch2D.Shape(
                        RayMarch2D.ShapeType.Circle,
                        new Vector2(1, 0),
                        new Vector2(1, 0),
                        RayMarch2D.ShapeEnv.Scene
                    )
                }
            );
        } else {
            main3 = new RayMarch3D(
                new Vector3(0, 3, 0),
                new RayMarch3D.Shape[] {
                    new RayMarch3D.Shape(
                        RayMarch3D.ShapeType.Cube,
                        new Vector3(0, 0, 0),
                        new Vector3(10, 0.1f, 10),
                        RayMarch3D.ShapeEnv.Scene
                    ),
                    new RayMarch3D.Shape(
                        RayMarch3D.ShapeType.Sphere,
                        new Vector3(0, 2, 3),
                        new Vector3(2, 0, 0),
                        RayMarch3D.ShapeEnv.Scene
                    )
                }
            );
        }
    }

    void Update()
    {
        if (!THREE_DIM)
            main2.March(0.01f, 10f, (float)angle, true);
        else
            main3.March(0.01f, 10f, new Vector2(angle, angle2), true);
    }
}