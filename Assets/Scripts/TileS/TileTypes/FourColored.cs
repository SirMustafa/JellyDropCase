using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourColored : TileBase
{
    protected override void ApplyColors()
    {
        Color randomColor = currentTileType.Colors[Random.Range(0, currentTileType.Colors.Count)];
        randomColor.a = 1f;

        for (int i = 0; i < 4; i++)
        {
            childsSpriteRenderer[i].color = randomColor;
        }
    }
}