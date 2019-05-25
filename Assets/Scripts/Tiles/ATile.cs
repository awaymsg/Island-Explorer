using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATile : MonoBehaviour
{
    public float elevation;
    public int foodAmt;
    public bool hasEvent;
    public bool explored;
    public bool ismovable;
    [SerializeField]
    private GameObject moveablemaskref;
    private GameObject moveablemask;

    void Awake()
    {
        explored = false;
        if (this is WaterScript)
        {
            ismovable = false;
        }
    }

    public void OnMouseDown()
    {
        Debug.Log(elevation);
        for (int i = 0; i < GetComponent<MeshFilter>().mesh.vertices.Length; i++)
        {
            Debug.Log(transform.TransformPoint(GetComponent<MeshFilter>().mesh.vertices[i]));
        }
    }

    public void WithinRange()
    {
        moveablemask = Instantiate(moveablemaskref);
        Vector3 pos = transform.position;
        pos.y += 1.5001f;
        moveablemask.transform.position = pos;
    }

    public void ResetColor()
    {
        Destroy(moveablemask);
    }
}