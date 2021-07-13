using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates a mesh where each vertex is a point.
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PointCloud : MonoBehaviour
{

    private Mesh mesh;
    int numPoints = 0;

    // Creates the gameObject's mesh using the provided points and signal strength values
    public void CreateMesh(Vector3[] positions, Vector3[] RGBs)
    {
        numPoints = positions.Length;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int[] indices = new int[numPoints];
        Color[] colors = new Color[numPoints];
        for (int i = 0; i < numPoints; ++i)
        {
            indices[i] = i;
            // Map RGB color [0,255] to [0,1]
            colors[i] = new Color(RGBs[i][0]/254.0f, RGBs[i][1]/254.0f, RGBs[i][2]/ 254.0f);
        }
        // Bind point to mesh
        mesh.vertices = positions;
        mesh.colors = colors;
        mesh.SetIndices(indices, MeshTopology.Points, 0);
    }
}