using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorReplacement
{
    public Color fromColor;
    public Color toColor;
}

public class ImageColorReplacement : MonoBehaviour
{
    public ColorReplacement[] colorReplacements;

    public Texture2D[] frames; // Array to hold the frames of the animation
    public float framesPerSecond = 10.0f; // Speed of the animation

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                Texture2D modifiedTexture = ApplyColorReplacements(frames[i]);
                SetMaterialTexture(modifiedTexture);
                yield return new WaitForSeconds(1.0f / framesPerSecond);
            }
        }
    }

    Texture2D ApplyColorReplacements(Texture2D originalTexture)
    {
        Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height);

        for (int x = 0; x < originalTexture.width; x++)
        {
            for (int y = 0; y < originalTexture.height; y++)
            {
                Color pixelColor = originalTexture.GetPixel(x, y);
                foreach (ColorReplacement replacement in colorReplacements)
                {
                    if (pixelColor.Equals(replacement.fromColor))
                    {
                        pixelColor = replacement.toColor;
                        break;
                    }
                }
                newTexture.SetPixel(x, y, pixelColor);
            }
        }

        newTexture.Apply();
        return newTexture;
    }

    void SetMaterialTexture(Texture2D texture)
    {
        // Create a new material with the custom shader
        Material newMaterial = new Material(Shader.Find("Custom/TransparentTextureShader"));
        newMaterial.mainTexture = texture;

        // Apply the new material to the renderer
        rend.material = newMaterial;
    }
}