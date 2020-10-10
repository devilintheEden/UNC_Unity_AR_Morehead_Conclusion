using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames {
    public class BasicTextureEdit
    {
        public static Color[] ScaleTexture(Texture2D texture, int width, int height)
        {
            Color[] sourceColors = texture.GetPixels();
            Color[] newColors = new Color[width * height];
            float ratioX = 1f / ((float)width / (texture.width - 1));
            float ratioY = 1f / ((float)height / (texture.height - 1));
            for (int i = 0; i < height; i++)
            {
                int yFloor = (int)Mathf.Floor(i * ratioY);
                int colorY1 = yFloor * texture.width;
                int colorY2 = (yFloor + 1) * texture.width;
                int offset = i * width;
                for (int j = 0; j < width; j++)
                {
                    int xFloor = (int)Mathf.Floor(j * ratioX);
                    float xLerp = j * ratioX - xFloor;
                    newColors[offset + j] = ColorLerpUnclamped(ColorLerpUnclamped(sourceColors[colorY1 + xFloor], sourceColors[colorY1 + xFloor + 1], xLerp), ColorLerpUnclamped(sourceColors[colorY2 + xFloor], sourceColors[colorY2 + xFloor + 1], xLerp), i * ratioY - yFloor);
                }
            }
            texture.Resize(width, height);
            return newColors;
        }

        static Color ColorLerpUnclamped(Color color1, Color color2, float value)
        {
            return new Color(color1.r + (color2.r - color1.r) * value, color1.g + (color2.g - color1.g) * value, color1.b + (color2.b - color1.b) * value, color1.a + (color2.a - color1.a) * value);
        }

        public static Texture2D CropTexture(Texture2D texture, Vector2 crop, Vector2 offset)
        {
            if (crop.x <= 0f || crop.y <= 0f || offset.x < 0 || offset.y < 0 || offset.x + crop.x > texture.width || offset.y + crop.y > texture.height)
            {
                return texture;
            }
            Texture2D result = new Texture2D((int)crop.x, (int)crop.y);
            result.SetPixels(texture.GetPixels(Mathf.FloorToInt(offset.x), Mathf.FloorToInt(offset.y), Mathf.FloorToInt(crop.x), Mathf.FloorToInt(crop.y)));
            return result;
        }

        public static Color[] BrightenContrastOverlayBlendColorizeTexture(Color[] pixels, Color targetC, Texture2D baseTex)
        {
            int len = pixels.Length;
            float para_bright = 1.2f;
            float para_contrast = 1.2f;
            Color[] baseColor = baseTex.GetPixels();
            Color pixel;
            Color base_pixel;
            for (int x = 0; x < len; x++)
            {
                pixel = pixels[x];
                base_pixel = baseColor[x];
                //colorize & overlay blend
                float l = 0.21f * pixel.r + 0.715f * pixel.g + 0.075f * pixel.b;
                float r = targetC.r * l < 0.5 ? 2 * targetC.r * l * base_pixel.r : 1 - 2 * (1 - targetC.r * l) * (1 - base_pixel.r);
                float g = targetC.g * l < 0.5 ? 2 * targetC.g * l * base_pixel.g : 1 - 2 * (1 - targetC.g * l) * (1 - base_pixel.g);
                float b = targetC.b * l < 0.5 ? 2 * targetC.b * l * base_pixel.b : 1 - 2 * (1 - targetC.b * l) * (1 - base_pixel.b);
                //brighter
                r = r * para_bright < 1 ? r * para_bright : 1;
                g = g * para_bright < 1 ? g * para_bright : 1;
                b = b * para_bright < 1 ? b * para_bright : 1;
                //increase contrast
                r = r > 0.5 ? (r * para_contrast < 1 ? r * para_contrast : 1) : (r / para_contrast);
                g = g > 0.5 ? (g * para_contrast < 1 ? g * para_contrast : 1) : (g / para_contrast);
                b = b > 0.5 ? (b * para_contrast < 1 ? b * para_contrast : 1) : (b / para_contrast);
                pixels[x] = new Color(r, g, b, 1);
            }
            return pixels;
        }
    }
}
