using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInverted2DEdges : MonoBehaviour {

    public EdgeCollider2D target;
    public int NumEdges;
    public float Radius = 0.5f;
    public int Layers = 1;
    public float RadiusStep = 0.05f;

    // Use this for initialization
    void Start () {
        Vector2[] points = new Vector2[(NumEdges)*Layers + 1];
        int index = 0;
        for (int layer = 0; layer < Layers; layer++)
        {
            for (int i = 0; i < NumEdges; i++)
            {
                float angle = 2 * Mathf.PI * i / NumEdges;
                float x = Radius * Mathf.Cos(angle);
                float y = Radius * Mathf.Sin(angle);

                points[index++] = new Vector2(x, y);
            }
            Radius += RadiusStep;
        }
        points[index++] = new Vector2(Radius - RadiusStep, 0);
        target.points = points;
    }
}
