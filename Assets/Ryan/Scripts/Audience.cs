using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public Texture2D[] frames; // Array to hold the frames of the animation
    public float framesPerSecond = 10.0f; // Speed of the animation
    public Color colorToApply = Color.blue; // Color to apply to the image frames

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
                SetMaterialTexture(frames[i]);
                yield return new WaitForSeconds(1.0f / framesPerSecond);
            }
        }
    }

    void SetMaterialTexture(Texture2D texture)
    {
        // Create a new material with the combined shader
        Material newMaterial = new Material(Shader.Find("Custom/TransparentTextureShader"));
        newMaterial.mainTexture = texture;
        newMaterial.SetColor("_ColorToApply", colorToApply);

        // Apply the new material to the renderer
        rend.material = newMaterial;
    }
}