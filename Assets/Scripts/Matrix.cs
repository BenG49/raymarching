using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix
{
    public float[,] data;
    public int rows;
    public int cols;

    public Matrix() : this(1, 1, 0f) {}
    public Matrix(int r) : this(r, 1, 0f) {}
    public Matrix(int r, int c) : this(r, c, 0f) {}
    public Matrix(int r, int c, float d) {
        if (r < 1 || c < 1) {
            Debug.Log("ArgumentException - Constructor");
        } else {
            rows = r;
            cols = c;

            data = new float[cols,rows];
            Fill(d);
        }
    }

    public float this[int r, int c] {
        get => data[c, r];
        set => data[c, r] = value;
    }

    public Matrix(float[,] data) {
        this.data = data;

        rows = data.GetLength(1);
        cols = data.GetLength(0);
    }

    public void Fill(float n) {
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                data[i,j] = n;
            }
        }
    }

    public static Matrix operator* (Matrix a, float b)  => a.MultiplyBy(b);
    public static Matrix operator* (Matrix a, Matrix b) => a.MultiplyBy(b);
    public static Matrix operator* (Matrix a, Vector2 b) => a.MultiplyBy(b);
    public static Matrix operator* (Matrix a, Vector3 b) => a.MultiplyBy(b);
    
    private Matrix MultiplyBy(float b) => 
        Multiply(this, new Matrix(1, 1, b));
    private Matrix MultiplyBy(Matrix b) =>
        Multiply(this, b);
    private Matrix MultiplyBy(Vector3 b) =>
        Multiply(this, new Matrix(new float[,]{
            {b.x}, {b.y}, {b.z}
        }));
    private Matrix MultiplyBy(Vector2 b) =>
        Multiply(this, new Matrix(new float[,]{
            {b.x}, {b.y}
        }));

    private static Matrix Multiply(Matrix a, Matrix b) {
        if (b.cols != a.rows) {
            Debug.Log("ArgumentException - Multiply()");

            return new Matrix(1, 1, 0);
        } else {
            Matrix output = new Matrix(b.rows, a.cols);

            for (int x = 0; x < output.rows; x++)  {
                for (int y = 0; y < output.cols; y++)  {
                    for (int i = 0; i < b.cols/* or a.rows */; i++) {
                        output[x,y] += b[x,i] * a[i,y];
                    }
                }
            }

            return output;
        }
    }
}
