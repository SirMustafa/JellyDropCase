using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeColored : TileBase
{
    private enum Direction { Top, Bottom, Left, Right }

    protected override void ApplyColors()
    {
        Direction splitDirection = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);
        Color uniqueColor = GetRandomColor();
        Color sharedColor1 = GetRandomColor();
        Color sharedColor2 = GetRandomColor();

        ApplyColorsByDirection(splitDirection, uniqueColor, sharedColor1, sharedColor2);
    }

    private Color GetRandomColor()
    {
        Color color = currentTileType.Colors[Random.Range(0, currentTileType.Colors.Count)];
        color.a = 1f;
        return color;
    }

    private void ApplyColorsByDirection(Direction direction, Color unique, Color shared1, Color shared2)
    {
        switch (direction)
        {
            case Direction.Top:
                AssignColors(unique, unique, shared1, shared2);
                break;
            case Direction.Bottom:
                AssignColors(shared1, shared2, unique, unique);
                break;
            case Direction.Left:
                AssignColors(unique, shared1, unique, shared2);
                break;
            case Direction.Right:
                AssignColors(shared2, unique, shared1, unique);
                break;
        }
    }

    private void AssignColors(Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
    {
        childsSpriteRenderer[0].color = topLeft;
        childsSpriteRenderer[1].color = topRight;
        childsSpriteRenderer[2].color = bottomLeft;
        childsSpriteRenderer[3].color = bottomRight;
    }
}