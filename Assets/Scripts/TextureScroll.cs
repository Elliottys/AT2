using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public Renderer renderererer;
    public Vector2 scrollSpeed;

    private void FixedUpdate()
    {
        // Credit to the_Simian https://discussions.unity.com/t/simple-scrolling-texture/443531/2
        renderererer.material.mainTextureOffset = scrollSpeed * Time.time;
    }
}
