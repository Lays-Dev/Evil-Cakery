using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
    public class Swatch
        {
            [SerializeField] private Color palette = new Color(1f, 0f, 0f, 1f); // R, G, B, A values from 0.0 to 1.0
        };

public class ColorPaletteManager : MonoBehaviour
{
    [SerializeField] private float totalEvil;

    public List<Swatch> goodPalette = new List<Swatch>(); //Holds color palettes for the good colors

    public List<Swatch> evilPalette = new List<Swatch>(); //Holds color palettes for the evil colors

}
