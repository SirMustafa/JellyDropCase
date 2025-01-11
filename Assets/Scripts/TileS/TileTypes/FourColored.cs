using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourColored : TileBase
{
    protected override void ApplyColors()
    {
        Color randomColor = currentTileType.Colors[Random.Range(0, currentTileType.Colors.Count)];
        randomColor.a = 1f;
        topLeft.GetComponent<SpriteRenderer>().color = randomColor;
        topRight.GetComponent<SpriteRenderer>().color = randomColor;
        botLeft.GetComponent<SpriteRenderer>().color = randomColor;
        botRight.GetComponent<SpriteRenderer>().color = randomColor;
    }
}