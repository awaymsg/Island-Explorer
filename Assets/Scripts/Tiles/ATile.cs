using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATile : MonoBehaviour
{
    public float elevation;
    public int foodAmt;
    public bool hasEvent;

    public void OnMouseDown()
    {
        Debug.Log(elevation);
        for (int i = 0; i < GetComponent<MeshFilter>().mesh.vertices.Length; i++)
        {
            Debug.Log(transform.TransformPoint(GetComponent<MeshFilter>().mesh.vertices[i]));
        }
    }
}
