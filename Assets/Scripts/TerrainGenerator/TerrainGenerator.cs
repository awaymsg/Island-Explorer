using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject grass;
    public GameObject plains;
    public GameObject forest;
    public GameObject coast;
    public GameObject water;
    public GameObject mountain;
    public int randomSeed;
    public GameObject[,] tileArray;

    //map size
    public int xSize;
    public int zSize;

    //land chance inverted
    public float landchance;

    //[HideInInspector] public List<List<GameObject>> waterbodies;

    // Start is called before the first frame update
    void Start()
    {
        Random.seed = randomSeed;
        tileArray = new GameObject[zSize, xSize];
        //GameObject testpiece = Instantiate(plains);
        //testpiece.transform.position = Vector3.zero;
        GenerateIsland();
        FillOcean();
        GenerateElevation();
        GenerateRivers();
        GenerateBiomes();
        LevelWater();
        FillCoast();
        Elevate();
    }

    //generates biomes based on elevation
    void GenerateBiomes()
    {
        for (int i = 1; i < xSize - 1; i++)
        {
            for (int j = 1; j < zSize - 1; j++)
            {
                if (tileArray[i, j].gameObject.tag != "Water")
                {
                    float elev = tileArray[i, j].GetComponent<ATile>().elevation;
                    if (elev < 5)
                    {
                        tileArray[i, j] = ReplaceTile(tileArray[i, j], "Plains", i, j);
                    } else if (elev > 8 && elev <= 12)
                    {
                        tileArray[i, j] = ReplaceTile(tileArray[i, j], "Forest", i, j);
                    } else if (elev > 12)
                    {
                        tileArray[i, j] = ReplaceTile(tileArray[i, j], "Mountain", i, j);
                    }
                }
            }
        }
    }

    //private void GroupTiles()
    //{
    //    for (int i = 1; i < xSize - 1; i++) {
    //        for (int j = 1; j < zSize - 1; j++)
    //        {
    //            if (tileArray[i, j].gameObject.tag == "Water")
    //            {
    //                if (waterbodies.Count == 0)
    //                {
    //                    List<GameObject> waterbody;
    //                }
    //            }
    //        }
    //    }
    //}

    void LevelWater()
    {
        for (int i = 1; i < xSize - 1; i++)
        {
            for (int j = 1; j < zSize - 1; j++)
            {
                if (tileArray[i, j].gameObject.tag == "Water")
                {
                    tileArray[i, j].GetComponent<ATile>().elevation = (int)tileArray[i, j].GetComponent<ATile>().elevation;
                    if (tileArray[i, j].GetComponent<ATile>().elevation > 8)
                    {
                        tileArray[i, j].GetComponent<ATile>().elevation = 8f;
                    }
                    //make sure neighboring non-water tiles have higher elevation
                    if (tileArray[i, j + 1].gameObject.tag != "Water")
                    {
                        while (tileArray[i, j + 1].GetComponent<ATile>().elevation <= tileArray[i, j].GetComponent<ATile>().elevation)
                        {
                            tileArray[i, j + 1].GetComponent<ATile>().elevation += 0.25f;
                        }
                    }
                    if (tileArray[i, j - 1].gameObject.tag != "Water")
                    {
                        while (tileArray[i, j - 1].GetComponent<ATile>().elevation <= tileArray[i, j].GetComponent<ATile>().elevation)
                        {
                            tileArray[i, j - 1].GetComponent<ATile>().elevation += 0.25f;
                        }
                    }
                    if (tileArray[i + 1, j].gameObject.tag != "Water")
                    {
                        while (tileArray[i + 1, j].GetComponent<ATile>().elevation <= tileArray[i, j].GetComponent<ATile>().elevation)
                        {
                            tileArray[i + 1, j].GetComponent<ATile>().elevation += 0.25f;
                        }
                    }
                    if (tileArray[i - 1, j].gameObject.tag != "Water")
                    {
                        while (tileArray[i - 1, j].GetComponent<ATile>().elevation <= tileArray[i, j].GetComponent<ATile>().elevation)
                        {
                            tileArray[i - 1, j].GetComponent<ATile>().elevation += 0.25f;
                        }
                    }
                }
            }
        }
        //fix coastal waters
        for (int j = 1; j < zSize - 1; j ++)
        {
            if (tileArray[1, j].gameObject.tag == "Water")
            {
                tileArray[1, j].GetComponent<ATile>().elevation = tileArray[0, j].GetComponent<ATile>().elevation + Random.value * 0.5f;
            }
            if (tileArray[xSize - 2, j].gameObject.tag == "Water")
            {
                tileArray[xSize - 2, j].GetComponent<ATile>().elevation = 
                    tileArray[xSize - 1, j].GetComponent<ATile>().elevation + Random.value * 0.5f;
            }
        }
        for (int i = 1; i < xSize - 1; i++)
        {
            if (tileArray[i, 1].gameObject.tag == "Water")
            {
                tileArray[i, 1].GetComponent<ATile>().elevation = tileArray[i, 0].GetComponent<ATile>().elevation + Random.value * 0.5f;
            }
            if (tileArray[i, zSize - 2].gameObject.tag == "Water")
            {
                tileArray[i, zSize - 2].GetComponent<ATile>().elevation =
                    tileArray[i, zSize - 1].GetComponent<ATile>().elevation + Random.value * 0.5f;
            }
        }
    }

    //generate rivers
    void GenerateRivers()
    {
        bool up, down, left, right = false;
        for (int i = 2; i < xSize - 2; i++)
        {
            for (int j = 2; j < zSize - 2; j++)
            {
                if (tileArray[i, j].gameObject.tag == "Water")
                {
                    up = false;
                    down = false;
                    left = false;
                    right = false;
                    int count = 0;
                    //check if neighboring tiles are water
                    if (tileArray[i, j].GetComponent<ATile>().elevation > tileArray[i, j + 1].GetComponent<ATile>().elevation
                            && tileArray[i, j + 1].gameObject.tag != "Water")
                    {
                        up = true;
                        count++;
                    }
                    if (tileArray[i, j].GetComponent<ATile>().elevation > tileArray[i, j - 1].GetComponent<ATile>().elevation
                            && tileArray[i, j - 1].gameObject.tag != "Water")
                    {
                        down = true;
                        count++;
                    }
                    if (tileArray[i, j].GetComponent<ATile>().elevation > tileArray[i + 1, j].GetComponent<ATile>().elevation
                            && tileArray[i + 1, j].gameObject.tag != "Water")
                    {
                        right = true;
                        count++;
                    }
                    if (tileArray[i, j].GetComponent<ATile>().elevation > tileArray[i - 1, j].GetComponent<ATile>().elevation
                            && tileArray[i - 1, j].gameObject.tag != "Water")
                    {
                        left = true;
                        count++;
                    }
                    //if 2 or less neighboring tiles are water
                    if (count != 0 && count <= 2)
                    {
                        while (true)
                        {
                            float val = Random.value;
                            if (val <= 0.25f)
                            {
                                if (up)
                                {
                                    tileArray[i, j + 1] = ReplaceTile(tileArray[i, j + 1], "Water", i, j + 1);
                                    break;
                                }
                            }
                            else if (val > 0.25f && val <= 0.5f)
                            {
                                if (down)
                                {
                                    tileArray[i, j - 1] = ReplaceTile(tileArray[i, j - 1], "Water", i, j - 1);
                                    break;
                                }
                            }
                            else if (val > 0.5f && val <= 0.75f)
                            {
                                if (right)
                                {
                                    tileArray[i + 1, j] = ReplaceTile(tileArray[i + 1, j], "Water", i + 1, j);
                                    break;
                                }
                            }
                            else
                            {
                                if (left)
                                {
                                    tileArray[i - 1, j] = ReplaceTile(tileArray[i - 1, j], "Water", i - 1, j);
                                    break;
                                }
                            }
                        }
                    }
                    //if this tile is already close to the coast, fill in to coast
                    if (i <= 3)
                    {
                        for (int k = i; k > 0; k--)
                        {
                            tileArray[k - 1, j] = ReplaceTile(tileArray[k - 1, j], "Water", k - 1, j);
                        }
                    }
                    if (i >= xSize - 4)
                    {
                        for (int k = i; k < xSize - 1; k++)
                        {
                            tileArray[k + 1, j] = ReplaceTile(tileArray[k + 1, j], "Water", k + 1, j);
                        }
                    }
                    if (j <= 3)
                    {
                        for (int k = j; k > 0; k--)
                        {
                            tileArray[k, j - 1] = ReplaceTile(tileArray[k, j - 1], "Water", k, j - 1);
                        }
                    }
                    if (j >= zSize - 4)
                    {
                        for (int k = j; k < zSize - 1; k++)
                        {
                            tileArray[k, j + 1] = ReplaceTile(tileArray[k, j + 1], "Water", k, j + 1);
                        }
                    }
                }
            }
        }
    }

    //method for replacing tiles
    GameObject ReplaceTile(GameObject tile, string type, int i, int j)
    {
        float elev = tile.GetComponent<ATile>().elevation;
        GameObject replacedTile;
        Destroy(tile);
        if (type == "Water")
        {
            replacedTile = Instantiate(water);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        else if (type == "Forest")
        {
            replacedTile = Instantiate(forest);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        else if (type == "Plains")
        {
            replacedTile = Instantiate(plains);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        else if (type == "Coast")
        {
            replacedTile = Instantiate(coast);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        else if (type == "Mountain")
        {
            replacedTile = Instantiate(mountain);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        else
        {
            replacedTile = Instantiate(grass);
            replacedTile.transform.position = new Vector3(i, 0, j);
            replacedTile.GetComponent<ATile>().elevation = elev;
        }
        return replacedTile;
    }

    void GenerateIsland()
    {
        for (int i = 1; i < xSize - 1; i++)
        {
            //zPos = 1;
            for (int j = 1; j < zSize - 1; j++)
            {
                float rand = Random.value;
                if ((i < xSize / 4 - Random.value * xSize / 10 - i / 4 && j < zSize / 4 - Random.value * zSize / 10 - j / 4) 
                        || (i < xSize / 4 - Random.value * xSize / 10 - i / 4 && j > zSize / 4 * 3 + Random.value * zSize / 10 + (zSize - j) / 4) 
                        || (i > xSize / 4 * 3 + Random.value * xSize / 10 + (xSize - i) / 4 && j < zSize / 4 - Random.value * zSize / 10 - j / 4)
                        || (i > xSize / 4 * 3 + Random.value * xSize / 10 + (xSize - i) / 4 && j > zSize / 4 * 3 + Random.value * zSize / 10 + (zSize - j) / 4)) {
                    if (rand > landchance * 2.5f)
                    {
                        tileArray[i, j] = Instantiate(grass);
                        tileArray[i, j].transform.position = new Vector3(i, 0, j);
                    }
                }
                else if ((i < xSize / 5 || j < zSize / 5) || (i > (xSize / 5) * 4 || j > (zSize / 5) * 4))
                {
                    if (rand > landchance)
                    {
                        tileArray[i, j] = Instantiate(grass);
                        tileArray[i, j].transform.position = new Vector3(i, 0, j);
                    }
                }
                else if ((i >= xSize / 5 || j >= zSize / 5) && ((i < xSize / 4 || j < zSize / 4))
                        || ((i <= xSize / 5 * 4) || (j <= zSize / 5 * 4)) && ((i > xSize / 4 * 3) || (j > zSize / 4 * 3))) {
                    if (rand > landchance * .9f)
                    {
                        tileArray[i, j] = Instantiate(grass);
                        tileArray[i, j].transform.position = new Vector3(i, 0, j);
                    }
                }
                else
                {
                    if (rand > landchance * .9f)
                    {
                        tileArray[i, j] = Instantiate(grass);
                        tileArray[i, j].transform.position = new Vector3(i, 0, j);
                    }
                }
                //zPos++;
            }
            //xPos++;
        }
        //fill island vertical
        for (int x = 2; x <= 4; x++) {
            for (int i = 1; i < xSize - 1; i++)
            {
                for (int j = 1; j < zSize - (x + 1); j += (x + 1))
                {
                    if (tileArray[i, j] != null && tileArray[i, j + x] != null)
                    {
                        for (int k = j; k <= j + x; k++)
                        {
                            Destroy(tileArray[i, k]);
                            tileArray[i, k] = Instantiate(grass);
                            tileArray[i, k].transform.position = new Vector3(i, 0, k);
                        }
                    }
                }
            }
        }
        //fill island horizontal
        for (int x = 2; x <= 4; x++)
        {
            for (int i = 1; i < xSize - (x + 1); i += (x + 1))
            {
                for (int j = 1; j < zSize - 1; j++)
                {
                    if (tileArray[i, j] != null && tileArray[i + x, j] != null)
                    {
                        for (int k = i; k <= i + x; k++)
                        {
                            Destroy(tileArray[k, j]);
                            tileArray[k, j] = Instantiate(grass);
                            tileArray[k, j].transform.position = new Vector3(k, 0, j);
                        }
                    }
                }
            }
        }
    }

    void FillOcean()
    {
        int xPos = 0;
        int zPos = 0;
        for (int i = 0; i < xSize; i++)
        {
            zPos = 0;
            for (int j = 0; j < zSize; j++)
            {
                if (tileArray[i, j] == null)
                {
                    tileArray[i, j] = Instantiate(water);
                    tileArray[i, j].transform.position = new Vector3(xPos, 0, zPos);
                }
                zPos++;
            }
            xPos++;
        }
    }

    void GenerateElevation()
    {
        float t = 0.6f;
        //generate elevation randomly
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                if ((i == 0 || j == 0) || ((i == xSize - 1) || (j == zSize - 1)))
                {
                    tileArray[i, j].GetComponent<ATile>().elevation = 3;
                }
                else if ((i < xSize / 8 || j < zSize / 8) || (i > xSize - (xSize / 8 + 1) || j > zSize - (zSize / 8 + 1)))
                {
                    if (tileArray[i, j].gameObject.tag == "Water")
                    {
                        tileArray[i, j].GetComponent<ATile>().elevation = Random.value * 2 + 2;
                    }
                    tileArray[i, j].GetComponent<ATile>().elevation = Random.value * 4 + 2;
                }
                else if (((i >= xSize / 8 - Random.value * xSize / 5 || j >= zSize / 8 - Random.value * zSize / 5) 
                        && (i < xSize / 3 - Random.value * xSize / 5 || j < zSize / 3 - Random.value * zSize / 5)) 
                        || (((i <= xSize - (xSize / 8 + 1) + Random.value * xSize / 5) 
                        || j <= zSize - (zSize / 8 + 1) + Random.value * zSize / 5) 
                        && (i > xSize - (xSize / 3 + 1) + Random.value * xSize / 5) 
                        || (j > zSize - (zSize / 3 + 1) + Random.value * zSize / 5)))
                {
                    if (tileArray[i, j].gameObject.tag == "Water")
                    {
                        tileArray[i, j].GetComponent<ATile>().elevation = Random.value * 4 + 2;
                    }
                    tileArray[i, j].GetComponent<ATile>().elevation = Random.value * 8;
                }
                else
                {
                    tileArray[i, j].GetComponent<ATile>().elevation = Random.value * 16 + 2;
                }
            }
        }

        LevelWater();

        //interpolate between points +3
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize - 3; j += 3)
            {
                tileArray[i, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j].GetComponent<ATile>().elevation,
                        tileArray[i, j + 3].GetComponent<ATile>().elevation, t);
                tileArray[i, j + 1].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j + 1].GetComponent<ATile>().elevation,
                        tileArray[i, j + 3].GetComponent<ATile>().elevation, t);
                tileArray[i, j + 2].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j + 2].GetComponent<ATile>().elevation,
                        tileArray[i, j + 3].GetComponent<ATile>().elevation, t);
                //runs again if elevation difference too high
                if (Mathf.Abs(tileArray[i, j].GetComponent<ATile>().elevation - tileArray[i, j + 1].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i, j + 1].GetComponent<ATile>().elevation - tileArray[i, j + 2].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i, j + 2].GetComponent<ATile>().elevation - tileArray[i, j + 3].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                j -= 3;
            }
        }
        for (int j = 0; j < zSize; j++)
        {
            for (int i = 0; i < xSize - 3; i += 3)
            {
                tileArray[i, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j].GetComponent<ATile>().elevation,
                        tileArray[i + 3, j].GetComponent<ATile>().elevation, t);
                tileArray[i + 1, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i + 1, j].GetComponent<ATile>().elevation,
                        tileArray[i + 3, j].GetComponent<ATile>().elevation, t);
                tileArray[i + 2, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i + 2, j].GetComponent<ATile>().elevation,
                        tileArray[i + 3, j].GetComponent<ATile>().elevation, t);
                //runs again if elevation difference too high
                if (Mathf.Abs(tileArray[i, j].GetComponent<ATile>().elevation - tileArray[i + 1, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i + 1, j].GetComponent<ATile>().elevation - tileArray[i + 2, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i + 2, j].GetComponent<ATile>().elevation - tileArray[i + 3, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                i -= 3;
            }
        }
        //interpolate between points -3
        for (int i = xSize - 1; i >= 0; i--)
        {
            for (int j = zSize - 2; j > 3; j -= 3)
            {
                tileArray[i, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j].GetComponent<ATile>().elevation,
                        tileArray[i, j - 3].GetComponent<ATile>().elevation, t);
                tileArray[i, j - 1].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j - 1].GetComponent<ATile>().elevation,
                        tileArray[i, j - 3].GetComponent<ATile>().elevation, t);
                tileArray[i, j - 2].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j - 2].GetComponent<ATile>().elevation,
                        tileArray[i, j - 3].GetComponent<ATile>().elevation, t);
                //runs again if elevation difference too high
                if (Mathf.Abs(tileArray[i, j].GetComponent<ATile>().elevation - tileArray[i, j - 1].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i, j - 1].GetComponent<ATile>().elevation - tileArray[i, j - 2].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i, j - 2].GetComponent<ATile>().elevation - tileArray[i, j - 3].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                j += 3;
            }
        }
        for (int j = zSize - 1; j >= 0; j--)
        {
            for (int i = xSize - 2; i > 3; i -= 3)
            {
                tileArray[i, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i, j].GetComponent<ATile>().elevation,
                        tileArray[i - 3, j].GetComponent<ATile>().elevation, t);
                tileArray[i - 1, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i - 1, j].GetComponent<ATile>().elevation,
                        tileArray[i - 3, j].GetComponent<ATile>().elevation, t);
                tileArray[i - 2, j].GetComponent<ATile>().elevation =
                        Mathf.Lerp(tileArray[i - 2, j].GetComponent<ATile>().elevation,
                        tileArray[i - 3, j].GetComponent<ATile>().elevation, t);
                //runs again if elevation difference too high
                if (Mathf.Abs(tileArray[i, j].GetComponent<ATile>().elevation - tileArray[i - 1, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i - 1, j].GetComponent<ATile>().elevation - tileArray[i - 2, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                if (Mathf.Abs(tileArray[i - 2, j].GetComponent<ATile>().elevation - tileArray[i - 3, j].GetComponent<ATile>().elevation) < 2)
                {
                    continue;
                }
                i += 3;
            }
        }
        
        //coastal waters elevation
        for (int i = 1; i < xSize - 2; i++)
        {
            tileArray[i, 0].GetComponent<ATile>().elevation = tileArray[i, 1].GetComponent<ATile>().elevation * 0.7f;
            tileArray[i, zSize - 1].GetComponent<ATile>().elevation = tileArray[i, zSize - 2].GetComponent<ATile>().elevation * 0.7f;
        }
        for (int j = 1; j < zSize - 2; j++)
        {
            tileArray[0, j].GetComponent<ATile>().elevation = tileArray[1, j].GetComponent<ATile>().elevation * 0.7f;
            tileArray[xSize - 1, j].GetComponent<ATile>().elevation = tileArray[xSize - 2, j].GetComponent<ATile>().elevation * 0.7f;
        }
        tileArray[0, 0].GetComponent<ATile>().elevation = tileArray[1, 0].GetComponent<ATile>().elevation * 0.7f;
        tileArray[xSize - 1, zSize - 1].GetComponent<ATile>().elevation = tileArray[xSize - 2, zSize - 1].GetComponent<ATile>().elevation * 0.7f;

        //interpolate coastal waters
        for (int i = 0; i < xSize - 3; i += 3)
        {
            //beginning tiles
            tileArray[i, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i, 0].GetComponent<ATile>().elevation,
                    tileArray[i + 3, 0].GetComponent<ATile>().elevation, t);
            tileArray[i + 1, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i + 1, 0].GetComponent<ATile>().elevation,
                    tileArray[i + 3, 0].GetComponent<ATile>().elevation, t);
            tileArray[i + 2, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i + 2, 0].GetComponent<ATile>().elevation,
                    tileArray[i + 3, 0].GetComponent<ATile>().elevation, t);
            //end tiles
            tileArray[i, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i + 3, zSize - 1].GetComponent<ATile>().elevation, t);
            tileArray[i + 1, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i + 1, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i + 3, zSize - 1].GetComponent<ATile>().elevation, t);
            tileArray[i + 2, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i + 2, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i + 3, zSize - 1].GetComponent<ATile>().elevation, t);
        }
        for (int j = 0; j < zSize - 3; j += 3)
        {
            //beginning tiles
            tileArray[0, j].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j].GetComponent<ATile>().elevation,
                    tileArray[0, j + 3].GetComponent<ATile>().elevation, t);
            tileArray[0, j + 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j + 1].GetComponent<ATile>().elevation,
                    tileArray[0, j + 3].GetComponent<ATile>().elevation, t);
            tileArray[0, j + 2].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j + 2].GetComponent<ATile>().elevation,
                    tileArray[0, j + 3].GetComponent<ATile>().elevation, t);
            //end tiles
            tileArray[xSize - 1, j].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j + 3].GetComponent<ATile>().elevation, t);
            tileArray[zSize - 1, j + 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j + 1].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j + 3].GetComponent<ATile>().elevation, t);
            tileArray[xSize - 1, j + 2].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j + 2].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j + 3].GetComponent<ATile>().elevation, t);
        }

        //interpolate coastal waters reverse
        for (int i = xSize - 1; i > 3; i -= 3)
        {
            //beginning tiles
            tileArray[i, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i, 0].GetComponent<ATile>().elevation,
                    tileArray[i - 3, 0].GetComponent<ATile>().elevation, t);
            tileArray[i - 1, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i - 1, 0].GetComponent<ATile>().elevation,
                    tileArray[i - 3, 0].GetComponent<ATile>().elevation, t);
            tileArray[i - 2, 0].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i - 2, 0].GetComponent<ATile>().elevation,
                    tileArray[i - 3, 0].GetComponent<ATile>().elevation, t);
            //end tiles
            tileArray[i, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i - 3, zSize - 1].GetComponent<ATile>().elevation, t);
            tileArray[i - 1, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i - 1, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i - 3, zSize - 1].GetComponent<ATile>().elevation, t);
            tileArray[i - 2, zSize - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[i - 2, zSize - 1].GetComponent<ATile>().elevation,
                    tileArray[i - 3, zSize - 1].GetComponent<ATile>().elevation, t);
        }
        for (int j = zSize - 1; j > 3; j -= 3)
        {
            //beginning tiles
            tileArray[0, j].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j].GetComponent<ATile>().elevation,
                    tileArray[0, j - 3].GetComponent<ATile>().elevation, t);
            tileArray[0, j - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j - 1].GetComponent<ATile>().elevation,
                    tileArray[0, j - 3].GetComponent<ATile>().elevation, t);
            tileArray[0, j - 2].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[0, j - 2].GetComponent<ATile>().elevation,
                    tileArray[0, j - 3].GetComponent<ATile>().elevation, t);
            //end tiles
            tileArray[xSize - 1, j].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j - 3].GetComponent<ATile>().elevation, t);
            tileArray[zSize - 1, j - 1].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j - 1].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j - 3].GetComponent<ATile>().elevation, t);
            tileArray[xSize - 1, j - 2].GetComponent<ATile>().elevation =
                    Mathf.Lerp(tileArray[xSize - 1, j - 2].GetComponent<ATile>().elevation,
                    tileArray[xSize - 1, j - 3].GetComponent<ATile>().elevation, t);
        }
    }

    void Elevate()
    {
        //implement elevation
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                //Debug.Log(tileArray[i, j].GetComponent<ATile>().elevation);
                tileArray[i, j].transform.position += new Vector3(0, tileArray[i, j].GetComponent<ATile>().elevation * 0.4f, 0);
            }
        }
    }

    void FillCoast()
    {
        for (int i = 1; i < xSize - 1; i++) {
            for (int j = 1; j < zSize - 1; j++)
            {
                if (tileArray[i, j].tag != "Water" && (tileArray[i, j - 1].tag == "Water" || tileArray[i, j + 1].tag == "Water"))
                {
                    if (tileArray[i, j].GetComponent<ATile>().elevation < 5)
                    {
                        if (Random.value > 0.2)
                        {
                            tileArray[i, j] = ReplaceTile(tileArray[i, j], "Coast", i, j);
                        }
                    }
                }
                if (tileArray[i, j].tag != "Water" && (tileArray[i - 1, j].tag == "Water" || tileArray[i + 1, j].tag == "Water"))
                {
                    if (tileArray[i, j].GetComponent<ATile>().elevation < 5)
                    {
                        if (Random.value > 0.2)
                        {
                            tileArray[i, j] = ReplaceTile(tileArray[i, j], "Coast", i, j);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
