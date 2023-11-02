using UnityEngine;
using System.Collections;

public class TerrainDeformationRealtime : MonoBehaviour
{
    public Terrain myTerrain;
    public int ringSize = 4;
    [Range(0.001f, 0.1f)]
    public float modificationSpeed = 0.01f;

    private int xResolution;
    private int zResolution;
    private float[,] heights;


    void Awake()
    {
        xResolution = myTerrain.terrainData.heightmapResolution;
        zResolution = myTerrain.terrainData.heightmapResolution;
        heights = myTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                raiseTerrain(hit.point);
            }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                lowerTerrain(hit.point);
            }
        }
    }

    private void raiseTerrain(Vector3 point)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - ringSize, terZ - ringSize, ringSize * 2 + 1, ringSize * 2 + 1);

        for (int tempY = 0; tempY < ringSize * 2 + 1; tempY++)
        {
            for (int tempX = 0; tempX < ringSize * 2 + 1; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - ringSize) + Mathf.Abs((float)tempX - ringSize);
                float maxDist = ringSize * 2;
                float proportion = dist_to_target / maxDist;

                height[tempX, tempY] += modificationSpeed * (1f - proportion);
                heights[terX - ringSize + tempX, terZ - ringSize + tempY] += modificationSpeed * (1f - proportion);
            }
        }

        myTerrain.terrainData.SetHeights(terX - ringSize, terZ - ringSize, height);
    }

    private void lowerTerrain(Vector3 point)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - ringSize, terZ - ringSize, ringSize * 2 + 1, ringSize * 2 + 1);

        for (int tempY = 0; tempY < ringSize * 2 + 1; tempY++)
        {
            for (int tempX = 0; tempX < ringSize * 2 + 1; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - ringSize) + Mathf.Abs((float)tempX - ringSize);
                float maxDist = ringSize * 2;
                float proportion = dist_to_target / maxDist;

                height[tempX, tempY] -= modificationSpeed * (1f - proportion);
                heights[terX - ringSize + tempX, terZ - ringSize + tempY] -= modificationSpeed * (1f - proportion);
            }
        }

        myTerrain.terrainData.SetHeights(terX - ringSize, terZ - ringSize, height);
    }
}

