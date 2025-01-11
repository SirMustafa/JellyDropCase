using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] protected TileStatsSO currentTileType;

    [SerializeField] protected GameObject topLeft;
    [SerializeField] protected GameObject topRight;
    [SerializeField] protected GameObject botLeft;
    [SerializeField] protected GameObject botRight;
    [SerializeField] protected List<Color> childColors = new List<Color>();
    bool isFulled = false;
    protected abstract void ApplyColors();

    private void OnEnable()
    {
        ApplyColors();
        UpdateChildColors();
    }

    private void UpdateChildColors()
    {
        childColors.Clear();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                childColors.Add(spriteRenderer.color);
            }
        }
    }

    public Color GetChildColor(int whichOne)
    {
        return childColors[whichOne];
    }

    public void SetChildColor(int whichOne, Color color)
    {
        if (whichOne >= 0 && whichOne < transform.childCount)
        {
            transform.GetChild(whichOne).GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void UpdateVisuals()
    {
        UpdateChildColors();
        CheckAndDestroy();
    }

    private void CheckAndDestroy()
    {
        if (!isFulled)
        {
            Color firstCornerColor = childColors[0];
            for (int i = 1; i < childColors.Count; i++)
            {
                if (childColors[i] != firstCornerColor)
                {
                    return;
                }
            }

            isFulled = true;
        }
        else
        {
            PlaceGenerator.Instance.DropTilesAbove((int)transform.position.x, (int)transform.position.y);
            UiManager.Uinstance.Score += 10;
            Destroy(gameObject);
        }
    }
}