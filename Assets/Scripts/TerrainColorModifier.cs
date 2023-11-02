using UnityEngine;

public class TerrainColorModifier : MonoBehaviour
{
    public Terrain terrain;
    public Color[] colors; // Arreglo de colores
    public float[] colorHeights; // Alturas correspondientes a los colores (normalizadas de 0 a 1)

    public Material terrainMaterial;

    private void Update()
    {
        UpdateTerrainColors();
    }

    private void UpdateTerrainColors()
    {
        TerrainData terrainData = terrain.terrainData;
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        Color[] splatmapColors = new Color[terrainData.alphamapWidth * terrainData.alphamapHeight];

        for (int z = 0; z < terrainData.alphamapHeight; z++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float normalizedHeight = heights[z, x];
                int chosenColor = 0;

                for (int i = 0; i < colorHeights.Length; i++)
                {
                    if (normalizedHeight >= colorHeights[i])
                    {
                        chosenColor = i;
                    }
                    else
                    {
                        break;
                    }
                }

                splatmapColors[z * terrainData.alphamapWidth + x] = colors[chosenColor];
            }
        }

        terrainMaterial.SetColorArray("_SplatmapColors", splatmapColors); // "_SplatmapColors" es el nombre de la propiedad de color en el shader
    }
}
