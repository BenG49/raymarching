using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quicktest : MonoBehaviour
{
    [SerializeField]
    public int angle1
    {
        get {return m_angle1; }
        set {m_angle1 = value; }
    }
    [Range(0, 360)]
    public int m_angle1;

    [SerializeField]
    public int angle2
    {
        get {return m_angle2; }
        set {m_angle2 = value; }
    }
    [Range(0, 360)]
    public int m_angle2;

    [SerializeField]
    public int angle3
    {
        get {return m_angle3; }
        set {m_angle3 = value; }
    }
    [Range(0, 360)]
    public int m_angle3;
    
    Vector3 origin = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(
            Vector3.zero,
            calcAngle(1f, new Vector3(angle1, angle2, angle3)),
            Color.white,
            0.01f
        );


        // Debug.DrawLine(
        //     Vector3.zero,
        //     calcAngle(1f, angle1, origin),
        //     Color.white,
        //     0.01f
        // );

        Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red, 0.01f);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 1, 0), Color.green, 0.01f);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 1), Color.blue, 0.01f);
    }

    private Vector3 calcAngle(float radius, Vector3 angle) {
        Vector3 r = angle * (Mathf.PI / 180);
        Vector3 output = new Vector3();
        Vector3 startPoint = new Vector3(1, 0, 1);

        float[] sin = new float[3] {Mathf.Sin(r.x), Mathf.Sin(r.y), Mathf.Sin(r.z)};
        float[] cos = new float[3] {Mathf.Cos(r.x), Mathf.Cos(r.y), Mathf.Cos(r.z)};

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
        Matrix o = new Matrix(new float[,]{
            {startPoint.x},
            {startPoint.y},
            {startPoint.z}
        });

        Matrix final = yaw*pitch*roll*o;

        output.x = final[0,0];
        output.y = final[0,1];
        output.z = final[0,2];

        output *= (1/Mathf.Sqrt(2));

        return output;
    }

    private Vector3 calcAngle(float radius, float angle, Vector2 origin) {
        float radians = angle * (Mathf.PI / 180);

        Matrix m = new Matrix(new float[,]{
            {Mathf.Cos(radians), -Mathf.Sin(radians)},
            {Mathf.Sin(radians), Mathf.Cos(radians)}
        });
        Matrix o = new Matrix(new float[,]{
            {origin.x},
            {origin.y}
        });

        Matrix final = m*o;

        Vector3 output = new Vector3();
        output.x = final[0,0];
        output.y = final[0,1];

        output *= (1/Mathf.Sqrt(2));

        return output;
    }
}