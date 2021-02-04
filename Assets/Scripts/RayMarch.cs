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
        public Vector2 size;
        public ShapeEnv shapeEnv;

        public Shape(ShapeType shapeType, Vector2 location, Vector2 size, ShapeEnv shapeEnv) {
            this.shapeType = shapeType;
            this.location = location;
            if (shapeType == ShapeType.Rectangle)
                size /= 2;
            this.size = size;
            this.shapeEnv = shapeEnv;
        }
    };

    private Vector2 cam;
    private Shape[] sceneShapes;
    private List<GameObject> marchShapes = new List<GameObject>();

    // default constructor
    public RayMarch2D() {
        cam = new Vector2(0, 0);
        sceneShapes = new Shape[] {
            new Shape(
                ShapeType.Rectangle,
                new Vector2(2, 0),
                new Vector2(1, 1),
                ShapeEnv.Scene
            )};
        
        Init();
    }
    public RayMarch2D(Vector2 cam, Shape[] sceneShapes) {
        this.cam = cam;
        this.sceneShapes = sceneShapes;

        Init();
    }

    private void Init() {
        // draw environment
        for (int i = 0; i < sceneShapes.Length; i++) {
            // if someone made the shape wrong
            sceneShapes[i].shapeEnv = ShapeEnv.Scene;
            drawShape(sceneShapes[i]);
        }
    }

    private void drawShape(Shape shape) {
        GameObject g;
        if (shape.shapeType == ShapeType.Circle) {
            g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = new Vector3(shape.size.x, 0.1f, shape.size.x);
        } else { // ShapeType.Rectangle
            g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.localScale = new Vector3(shape.size.x*2, 0.1f, shape.size.y*2);
        }

        g.transform.position = new Vector3(shape.location.x, 0f, shape.location.y);

        if (shape.shapeEnv == ShapeEnv.March) {
            marchShapes.Add(g);
        }
    }

    private void clearMarchDraw() {
        for (int i = 0; i < marchShapes.Count; i++) {
            Destroy(marchShapes[i]);
        }

        int len = marchShapes.Count;
        if (len > 0) {
            len--;

            for (int i = 0; i < len; i++) {
                marchShapes.RemoveAt(0);
            }
        }
    }

    /*
        NOTE: returns 0, 0 if there are no obstructions
        
        angle of 0 forms ray towards x=1
    */
    public Vector2 March(float min, float max, float angle, bool draw) {
        // converting to radians
        float radians = (float)((Mathf.PI / 180) * (angle % 360));
        Vector2 currentPoint = cam;
        // this is probably not neccessary and unoptimized, starts radius in avg of min and max
        float currentRadius = (max + min)/2;

        if (draw) {
            clearMarchDraw();
        }

        // if the circle exactly reaches shape
        while (currentRadius > min && currentRadius < max) {
            currentRadius = MarchStep(currentPoint, draw);
            
            /* sin(angle)*hypotenuse = opposite [y]
               cos(angle)*hypotenuse = adjacent [x] */
            currentPoint.x += Mathf.Cos(radians)*currentRadius;
            currentPoint.y += Mathf.Sin(radians)*currentRadius;
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
                    sceneShapes[i].size.x/2
                );
            } else if (sceneShapes[i].shapeType == ShapeType.Rectangle) {
                temp = Dist2D.RectDistance(
                    location,
                    sceneShapes[i].location,
                    new Vector2(sceneShapes[i].size.x, sceneShapes[i].size.y)
                );
            }

            if (temp < output || i == 0)
                output = temp;
        }

        if (draw) {
            drawShape(new Shape(
                ShapeType.Circle,
                location,
                new Vector2(output*2, output*2),
                ShapeEnv.March
            ));
        }

        return output;
    }
}

public class RayMarch3D : MonoBehaviour
{
    public enum ShapeType {Sphere, Cube};
    public enum ShapeEnv {Scene, March};

    public struct Shape {
        public ShapeType shapeType;
        public Vector3 location;
        public Vector3 size;
        public ShapeEnv shapeEnv;

        public Shape(ShapeType shapeType, Vector3 location, Vector3 size, ShapeEnv shapeEnv) {
            this.shapeType = shapeType;
            this.location = location;
            if (shapeType == ShapeType.Cube)
                size /= 2;
            this.size = size;
            this.shapeEnv = shapeEnv;
        }
    };

    private Vector2 cam;
    private Shape[] sceneShapes;
    private List<GameObject> marchShapes = new List<GameObject>();

    // default constructor
    public RayMarch3D() {
        cam = new Vector3(0, 0, 0);
        sceneShapes = new Shape[] {
            new Shape(
                ShapeType.Cube,
                new Vector3(2, 0, 0),
                new Vector3(1, 0, 1),
                ShapeEnv.Scene
            )};
        
        Init();
    }
    public RayMarch3D(Vector3 cam, Shape[] sceneShapes) {
        this.cam = cam;
        this.sceneShapes = sceneShapes;

        Init();
    }

    private void Init() {
        // draw environment
        for (int i = 0; i < sceneShapes.Length; i++) {
            // if someone made the shape wrong
            sceneShapes[i].shapeEnv = ShapeEnv.Scene;
            drawShape(sceneShapes[i]);
        }
    }

    private void drawShape(Shape shape) {
        GameObject g;
        if (shape.shapeType == ShapeType.Sphere) {
            g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = new Vector3(shape.size.x, shape.size.x, shape.size.x);
        } else { // ShapeType.Cube
            g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            g.transform.localScale = new Vector3(shape.size.x*2, shape.size.y*2, shape.size.z*2);
        }

        g.transform.position = new Vector3(shape.location.x, shape.location.y, shape.location.z);

        if (shape.shapeEnv == ShapeEnv.March) {
            marchShapes.Add(g);
        }
    }

    private void clearMarchDraw() {
        for (int i = 0; i < marchShapes.Count; i++) {
            Destroy(marchShapes[i]);
        }

        int len = marchShapes.Count;
        if (len > 0) {
            len--;

            for (int i = 0; i < len; i++) {
                marchShapes.RemoveAt(0);
            }
        }
    }

    /*
        NOTE: returns (0, 0, 0) if there are no obstructions
    */
    public Vector2 March(float min, float max, Vector3 angle, bool draw) {
        Vector3 radians = angle*(Mathf.PI/180);
        Vector3 currentPoint = cam;
        // this is probably not neccessary and unoptimized, starts radius in avg of min and max
        float currentRadius = (max + min)/2;

        if (draw) {
            clearMarchDraw();
        }

        // if the circle exactly reaches shape
        while (currentRadius > min && currentRadius < max) {
            currentRadius = MarchStep(currentPoint, draw, min, max);
            currentPoint += Rotation.Rot3D(angle, new Vector3(currentRadius, 0, 0));
        }

        if (currentRadius <= min)
            return currentPoint;
        else
            return new Vector3(0, 0, 0);
    }

    public float MarchStep(Vector3 location, bool draw, float minDraw, float maxDraw) {
        float output = 0;
        float temp = 0;

        for (int i = 0; i < sceneShapes.Length; i++) {
            if (sceneShapes[i].shapeType == ShapeType.Sphere) {
                temp = Dist3D.SphereDistance(
                    location,
                    sceneShapes[i].location,
                    sceneShapes[i].size.x/2
                );
            } else if (sceneShapes[i].shapeType == ShapeType.Cube) {
                temp = Dist3D.CubeDistance(
                    location,
                    sceneShapes[i].location,
                    new Vector3(sceneShapes[i].size.x, sceneShapes[i].size.y, sceneShapes[i].size.z)
                );
            }

            if (temp < output || i == 0)
                output = temp;
        }

        if (draw && output > minDraw && output < maxDraw) {
            drawShape(new Shape(
                ShapeType.Sphere,
                location,
                new Vector3(output*2, output*2, output*2),
                ShapeEnv.March
            ));
        }

        return output;
    }
}