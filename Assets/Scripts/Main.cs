using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    public int angle
    {
        get {return m_angle; }
        set {m_angle = value; }
    }
    [Range(0, 360)]
    public int m_angle;
 
    private int prevAngle = 0;

    private RayMarch2D main;

    // Start is called before the first frame update
    void Start()
    {
        main = new RayMarch2D(
            new Vector2(0, 0),
            new RayMarch2D.Shape[] {
                new RayMarch2D.Shape(
                    RayMarch2D.ShapeType.Rectangle,
                    new Vector2(0, 2),
                    1f, 1f,
                    RayMarch2D.ShapeEnv.Scene
                )
            }
        );

        main.March(0.01f, 25f, 0f, true);
        main.March(0.01f, 25f, 90f, true);
        // print(main.March(0.01f, 25f, 180f, true));
    }

    // Update is called once per frame
    void Update()
    {
        prevAngle = angle;
        // update angle
        // if (angle !=  prevAngle) {
            // main.March(0.01f, 25f, (float)angle, true);
        // }
    }
}