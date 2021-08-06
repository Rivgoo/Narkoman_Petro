using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Core.GameUI
{
    public static class ColorChanger
    {
        public static Color Lerp(UnityEngine.UI.Graphic colorSource, Color target, float speed)
        {
            return Color.Lerp(colorSource.color, target, speed * Time.deltaTime);
        }

        public static Color Lerp(GameUI.Text.Letter colorSource, Color target, float speed)
        {
            return Color.Lerp(colorSource.Color, target, speed * Time.deltaTime);
        }

        public static Color Lerp(Color source, Color target, float speed)
        {
            return Color.Lerp(source, target, speed * Time.deltaTime);
        }

        public static Color SetAlphaChannel(Color source, float targetAlfa)
        {
            return new Color(source.r, source.g, source.b, targetAlfa);
        }

        public static Color GetRandomColor()
        {
            var r = Random.Range(0, 255f) / 255;
            var b = Random.Range(0, 255f) / 255;
            var g = Random.Range(0, 255f) / 255;

            return new Color(r, b, g, 1);
        }
    }
}
