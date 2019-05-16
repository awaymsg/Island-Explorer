using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMesh : MonoBehaviour
{
    public Material material;

    private Mesh mesh;
    private MeshRenderer rendererer;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        rendererer = GetComponent<MeshRenderer>();
        rendererer.material = material;
        material.color = Color.green;
    }

    public void DrawMesh(List<int> trianglesList, List<Vector3> verts)
    {
        int[] tris = new int[trianglesList.Count];
        Vector3[] vertices = new Vector3[verts.Count];
        for (int i = 0; i < trianglesList.Count; i++)
        {
            tris[i] = trianglesList[i];
        }
        for (int i = 0; i < verts.Count; i++)
        {
            vertices[i] = verts[i];
            Debug.Log(vertices[i]);
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
