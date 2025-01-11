using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullColored : TileBase
{
    protected override void ApplyColors()
    {
        List<Color> availableColors = new List<Color>(currentTileType.Colors);
        GameObject[] childObjects = { topLeft, topRight, botLeft, botRight };

        foreach (var child in childObjects)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            Color randomColor = availableColors[randomIndex];
            randomColor.a = 1f;
            availableColors.RemoveAt(randomIndex);
            child.GetComponent<SpriteRenderer>().color = randomColor;
        }
    }
}