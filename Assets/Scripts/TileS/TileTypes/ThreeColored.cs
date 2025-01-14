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

                childsSpriteRenderer[0].color = uniqueColor;
                childsSpriteRenderer[1].color = uniqueColor;
                childsSpriteRenderer[2].color = sharedColor1;
                childsSpriteRenderer[3].color = sharedColor2;

                break;

            case Direction.Bottom:

                childsSpriteRenderer[0].color = sharedColor1;
                childsSpriteRenderer[1].color = sharedColor2;
                childsSpriteRenderer[2].color = uniqueColor;
                childsSpriteRenderer[3].color = uniqueColor;
                break;

            case Direction.Left:

                childsSpriteRenderer[0].color = uniqueColor;
                childsSpriteRenderer[1].color = sharedColor1;
                childsSpriteRenderer[2].color = uniqueColor;
                childsSpriteRenderer[3].color = sharedColor2;
                break;

            case Direction.Right:

                childsSpriteRenderer[0].color = sharedColor2;
                childsSpriteRenderer[1].color = uniqueColor;
                childsSpriteRenderer[2].color = sharedColor1;
                childsSpriteRenderer[3].color = uniqueColor;
                break;
        }
    }
}