using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMarch2D : MonoBehaviour
{
    public enum ShapeType {Circle, Rectangle};
    public enum ShapeEnv {Scene, March};
    public struct Shape {
        public ShapeType shapeType;
        public Vector2 location;
        public float size1;
        public float size2;
        public ShapeEnv shapeEnv;

        public Shape(ShapeType shapeType, Vector2 location, float size1, float size2, ShapeEnv shapeEnv) {
            this.shapeType = shapeType;
            this.location = location;
            this.size1 = size1;
            this.size2 = size2;
            this.shapeEnv = shapeEnv;
        }
    };

    private Vector2 cam;
    private Shape[] sceneShapes;
    private List<GameObject> marchShapes = new List<GameObject>();

    // default constructor
    public RayMarch2D() {
        cam = new Vector2(0, 0);
        sceneShapes = new Shape[] {new Shape(
                    ShapeType.Rectangle,
                    new Vector2(2, 0),
                    1f,
                    1f,
                    ShapeEnv.Scene
                 )};
        
        drawEnv();
    }
    public RayMarch2D(Vector2 cam, Shape[] sceneShapes) {
        this.cam = cam;
        this.sceneShapes = sceneShapes;

        drawEnv();
    }

    private void drawEnv() {
        for (int i = 0; i < sceneShapes.Length; i++) {
            drawShape(sceneShapes[i]);
        }
    }


    private void drawShape(Shape shape) {
        GameObject g = new GameObject();
        if (shape.shapeType == ShapeType.Circle) {
            g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = new Vector3(shape.size1, 0.1f, shape.size2);
        } else if (shape.shapeType == ShapeType.Rectangle) {
            g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.localScale = new Vector3(shape.size1*2, 0.1f, shape.size2*2);
        }

        g.transform.position = new Vector3(shape.location.x, 0f, shape.location.y);

        if (shape.shapeEnv == ShapeEnv.March) {
            marchShapes.Add(g);
        }
    }

    private void clearMarchDraw() {
        for (int i = 0; i < marchShapes.Count; i++)
            Destroy(marchShapes[i]);

        int len = marchShapes.Count;
        if (len > 0) {
            len--;
            print(len);

            for (int i = 0; i < len; i++)
                marchShapes.RemoveAt(0);
        }
    }

    /*
        NOTE: returns 0, 0 if there are no obstructions
        
        angle of 0 forms ray towards x=1
    */
    public Vector2 March(float min, float max, float angle, bool draw) {
        // converting to radians
        float radians = (float)((Math.PI / 180) * (angle % 360));
        Vector2 currentPoint = cam;
        // this is probably not neccessary and unoptimized, starts radius in avg of min and max
        float currentRadius = (max + min)/2;

        if (draw) {
            print("clear march draw");
            clearMarchDraw();
        }

        // if the circle exactly reaches shape
        while (currentRadius > min && currentRadius < max) {
            currentRadius = MarchStep(currentPoint, draw);
            
            /* sin(angle)*hypotenuse = opposite [y]
               cos(angle)*hypotenuse = adjacent [x] */
            currentPoint.x += (float)Math.Cos(radians)*currentRadius;
            currentPoint.y += (float)Math.Sin(radians)*currentRadius;
        }

        if (currentRadius <= min)
            return currentPoint;
        // else if (currentRadius >= max)
        else
            return new Vector2(0, 0);
    }

    public float MarchStep(Vector2 location, bool draw) {
        float output = 0;
        float temp = 0;

        for (int i = 0; i < sceneShapes.Length; i++) {
            if (sceneShapes[i].shapeType == ShapeType.Circle) {
                temp = Dist2D.CircleDistance(
                    location,
                    sceneShapes[i].location,
                    sceneShapes[i].size1
                );
            } else if (sceneShapes[i].shapeType == ShapeType.Rectangle) {
                temp = Dist2D.RectDistance(
                    location,
                    sceneShapes[i].location,
                    new Vector2(sceneShapes[i].size1, sceneShapes[i].size2)
                );
            }

            if (temp > output)
                output = temp;
        }

        if (draw) {
            drawShape(new Shape(
                ShapeType.Circle,
                location,
                output*2,
                output*2,
                ShapeEnv.March
            ));
        }

        return output;
    }
}