using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeColored : TileBase
{
    private enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    protected override void ApplyColors()
    {
        Direction splitDirection = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);

        List<Color> availableColors = new List<Color>(currentTileType.Colors);
        Color uniqueColor = availableColors[Random.Range(0, availableColors.Count)];
        uniqueColor.a = 1f;
        availableColors.Remove(uniqueColor);

        Color sharedColor1 = availableColors[Random.Range(0, availableColors.Count)];
        sharedColor1.a = 1f;
        availableColors.Remove(sharedColor1);
        Color sharedColor2 = availableColors[Random.Range(0, availableColors.Count)];
        sharedColor2.a = 1f;

        switch (splitDirection)
        {
            case Direction.Top:

                topLeft.GetComponent<SpriteRenderer>().color = uniqueColor;
                topRight.GetComponent<SpriteRenderer>().color = uniqueColor;
                botLeft.GetComponent<SpriteRenderer>().color = sharedColor1;
                botRight.GetComponent<SpriteRenderer>().color = sharedColor2;
                break;

            case Direction.Bottom:

                topLeft.GetComponent<SpriteRenderer>().color = sharedColor1;
                topRight.GetComponent<SpriteRenderer>().color = sharedColor2;
                botLeft.GetComponent<SpriteRenderer>().color = uniqueColor;
                botRight.GetComponent<SpriteRenderer>().color = uniqueColor;
                break;

            case Direction.Left:

                topLeft.GetComponent<SpriteRenderer>().color = uniqueColor;
                botLeft.GetComponent<SpriteRenderer>().color = uniqueColor;
                topRight.GetComponent<SpriteRenderer>().color = sharedColor1;
                botRight.GetComponent<SpriteRenderer>().color = sharedColor2;
                break;

            case Direction.Right:

                topLeft.GetComponent<SpriteRenderer>().color = sharedColor1;
                botLeft.GetComponent<SpriteRenderer>().color = sharedColor2;
                topRight.GetComponent<SpriteRenderer>().color = uniqueColor;
                botRight.GetComponent<SpriteRenderer>().color = uniqueColor;
                break;
        }
    }
}