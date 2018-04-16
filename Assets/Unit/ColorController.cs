using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public int maxLevel;
    public int level;

    private Texture2D baseTexture;

    public Texture2D effectLayer;
    private Texture2D copiedEffectLayer;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseTexture = new Texture2D(effectLayer.width, effectLayer.height, TextureFormat.RGBA32, true);

        copiedEffectLayer = effectGradient();
        combineTexture(baseTexture, copiedEffectLayer);

        spriteRenderer.sprite = Sprite.Create(baseTexture, new Rect(), new Vector2(), 1475);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void combineTexture(Texture2D original, Texture2D effect)
    {

        int startX = 0;
        int startY = original.height - effect.height;

        for (int x = startX; x < original.width; x++)
        {
            for (int y = startY; y < original.height; y++)
            {
                Color bgColor = original.GetPixel(x, y);
                Color wmColor = effect.GetPixel(x - startX, y - startY);

                Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

                original.SetPixel(x, y, final_color);
            }
        }

        original.Apply();
    }

    private Texture2D effectGradient()
    {
        Texture2D texture = new Texture2D(effectLayer.width, effectLayer.height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        
        for (int y = 0; y < texture.height; y++)
        {
            for(int x = 0; x < texture.height; x++)
            {
                Color currentPixelColor = texture.GetPixel(x, y);
                Color newPixelColor;

                float hue = 0;
                float saturation = 0;
                float value = 0;

                float levelIncrease;

                Color.RGBToHSV(currentPixelColor, out hue, out saturation, out value);

                if (level < maxLevel / 2)
                {
                    levelIncrease = level / (maxLevel / 2);

                    newPixelColor = Color.HSVToRGB(hue, saturation, value + levelIncrease);

                    texture.SetPixel(x, y, newPixelColor);
                }
                else
                {
                    levelIncrease = (level - maxLevel / 2)  / (maxLevel / 2);

                    newPixelColor = Color.HSVToRGB(hue, saturation, value + levelIncrease);

                    texture.SetPixel(x, y, newPixelColor);
                }
            }
        }

        texture.Apply();

        return texture;
    }

}
