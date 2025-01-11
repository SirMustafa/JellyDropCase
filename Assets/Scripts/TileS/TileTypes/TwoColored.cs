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

            topLeft.GetComponent<SpriteRenderer>().color = color1;
            topRight.GetComponent<SpriteRenderer>().color = color1;
            botLeft.GetComponent<SpriteRenderer>().color = color2;
            botRight.GetComponent<SpriteRenderer>().color = color2;
        }
        else
        {

            topLeft.GetComponent<SpriteRenderer>().color = color1;
            botLeft.GetComponent<SpriteRenderer>().color = color1;
            topRight.GetComponent<SpriteRenderer>().color = color2;
            botRight.GetComponent<SpriteRenderer>().color = color2;
        }
    }
}