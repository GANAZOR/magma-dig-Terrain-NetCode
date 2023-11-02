using UnityEngine;

public class SphereTerrainModifier : MonoBehaviour
{
    private Terrain terrain;
    public int ringSize = 4;
    int ringSizeBase;

    private bool contactTerrain;


    public float modificationSpeed = 0.01f;

    private void Awake()
    {
        terrain = FindObjectOfType<Terrain>();
        ringSizeBase = ringSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && contactTerrain)
        {
            ModifyTerrain(true);
        }
        else if (Input.GetMouseButtonDown(1) && contactTerrain)
        {
            ModifyTerrain(false);
            ringSize = ringSize + 1;
        }
        else ringSize = ringSizeBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TerrainCollider"))
        {
            contactTerrain = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TerrainCollider"))
        {
            contactTerrain = false;
        }
    }

    private void ModifyTerrain(bool raise)
    {
        int terX = (int)((transform.position.x / terrain.terrainData.size.x) * terrain.terrainData.heightmapResolution);
        int terZ = (int)((transform.position.z / terrain.terrainData.size.z) * terrain.terrainData.heightmapResolution);

        float[,] height = terrain.terrainData.GetHeights(terX - ringSize, terZ - ringSize, ringSize * 2 + 1, ringSize * 2 + 1);

        for (int tempY = 0; tempY < ringSize * 2 + 1; tempY++)
        {
            for (int tempX = 0; tempX < ringSize * 2 + 1; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - ringSize) + Mathf.Abs((float)tempX - ringSize);
                float maxDist = ringSize * 2;
                float proportion = dist_to_target / maxDist;

                if (raise)
                {
                    height[tempX, tempY] += modificationSpeed * (1f - proportion);
                }
                else
                {
                    height[tempX, tempY] -= modificationSpeed * (1f - proportion);
                }
            }
        }

        terrain.terrainData.SetHeights(terX - ringSize, terZ - ringSize, height);
    }
}
