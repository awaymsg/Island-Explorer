using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour
{
    private GameObject[,] tileArray;
    private int x;
    private int z;
    private int xSize;
    private int zSize;
    private GameObject currenttile;

    public void setTileArray(GameObject[,] tilearray)
    {
        tileArray = tilearray;
    }

    public TerrainGrid(GameObject[,] tilearray, int xsize, int zsize)
    {
        tileArray = tilearray;
        xSize = xsize;
        zSize = zsize;
        SetStartingTile();
    }

    public GameObject GetCurrentTile()
    {
        return currenttile;
    }

    public GameObject GetNextTile(int direction)
    {
        ResetMoveableTiles();
        if (direction == 0)
        {
            if (z + 1 < zSize)
            {
                z++;
            }
        } else if (direction == 1)
        {
            if (z - 1 >= 0)
            {
                z--;
            }
        } else if (direction == 2)
        {
            if (x + 1 < xSize)
            {
                x++;
            }
        } else if (direction == 3)
        {
            if (x - 1 >= 0)
            {
                x--;
            }
        }
        currenttile = tileArray[x, z];
        SetMoveableTiles();
        return currenttile;
    }

    void SetMoveableTiles()
    {
        if (z + 1 < zSize)
        {
            tileArray[x, z + 1].GetComponent<ATile>().WithinRange();
        }
        if (z - 1 >= 0)
        {
            tileArray[x, z - 1].GetComponent<ATile>().WithinRange();
        }
        if (x + 1 < xSize)
        {
            tileArray[x + 1, z].GetComponent<ATile>().WithinRange();
        }
        if (x - 1 >= 0)
        {
            tileArray[x - 1, z].GetComponent<ATile>().WithinRange();
        }
    }

    void ResetMoveableTiles()
    {
        if (z + 1 < zSize)
        {
            tileArray[x, z + 1].GetComponent<ATile>().ResetColor();
        }
        if (z - 1 >= 0)
        {
            tileArray[x, z - 1].GetComponent<ATile>().ResetColor();
        }
        if (x + 1 < xSize)
        {
            tileArray[x + 1, z].GetComponent<ATile>().ResetColor();
        }
        if (x - 1 >= 0)
        {
            tileArray[x - 1, z].GetComponent<ATile>().ResetColor();
        }
    }

    void SetStartingTile()
    {
        for (int j = 1; j < zSize - 1; j++)
        {
            if (tileArray[1, j].gameObject.tag == "Coast")
            {
                currenttile = tileArray[1, j];
                x = 1;
                z = j;
                break;
            }
        }
    }
}
