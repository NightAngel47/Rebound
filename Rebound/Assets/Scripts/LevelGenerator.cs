/* Primary Author: Trent Lewis
 */

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public bool isHowToPlay = false;
    public ColorToPrefab[] colorMappings;
    public PostProcessingEffects postProcessEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null && !isHowToPlay)
            map = GameManager.instance.levels[GameManager.instance.nextLevel];
        GenerateLevel();
        EnablePostProcessing();
    }

    void EnablePostProcessing()
    {
        postProcessEffect = FindObjectOfType<PostProcessingEffects>();

        if (GameManager.instance.nextLevel <= 2)
        {
            postProcessEffect.DisableBloom();
        }
        else if (GameManager.instance.nextLevel > 2)
        {
            postProcessEffect.EnableBloom();
        }
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile (int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            // The Pixel is transparrent.
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                GameObject newGO = Instantiate(colorMapping.prefab, position, Quaternion.identity); 
                if (newGO.CompareTag("Platform") || newGO.CompareTag("Hazard"))
                {
                    newGO.transform.SetParent(transform);
                }
            }
        }
    }
}
