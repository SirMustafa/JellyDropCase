using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoColored : TileBase
{
    private enum Direction
    {
        Horizontal,
        Vertical
    }

    protected override void ApplyColors()
    {
        Direction splitDirection = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);

        List<Color> availableColors = new List<Color>(currentTileType.Colors);
        Color color1 = availableColors[Random.Range(0, availableColors.Count)];
        color1.a = 1f;
        availableColors.Remove(color1);
        Color color2 = availableColors[Random.Range(0, availableColors.Count)];
        color2.a = 1f;

        if (splitDirection == Direction.Horizontal)
        {

            childsSpriteRenderer[0].color = color1;
            childsSpriteRenderer[1].color = color1;
            childsSpriteRenderer[2].color = color2;
            childsSpriteRenderer[3].color = color2;
        }
        else
        {

            childsSpriteRenderer[0].color = color2;
            childsSpriteRenderer[1].color = color2;
            childsSpriteRenderer[2].color = color1;
            childsSpriteRenderer[3].color = color1;
        }
    }
}