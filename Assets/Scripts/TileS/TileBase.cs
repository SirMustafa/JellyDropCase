using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    [SerializeField] protected TileStatsSO currentTileType;
    [SerializeField] protected List<GameObject> childs = new List<GameObject>();
    protected List<SpriteRenderer> childsSpriteRenderer = new List<SpriteRenderer>();

    protected abstract void ApplyColors();

    private void OnEnable()
    {
        GetAllSpriteRenderers();
        ApplyColors();
    }
    void GetAllSpriteRenderers()
    {
        for (int i = 0; i < 4; i++)
        {
            childsSpriteRenderer.Add(childs[i].GetComponent<SpriteRenderer>());
        }
    }

    public Color GetChildColor(int whichOne)
    {
        return childsSpriteRenderer[whichOne].color;
    }

    public void SetChildColor(int whichOne, Color color)
    {
        if (whichOne >= 0 && whichOne < 4)
        {
            childsSpriteRenderer[whichOne].color = color;
        }
    }

    public void UpdateVisuals()
    {
        CheckAndDestroy();
    }

    private void CheckAndDestroy()
    {
        Color firstCornerColor = childsSpriteRenderer[0].color;
        for (int i = 1; i < 4; i++)
        {
            if (childsSpriteRenderer[i].color != firstCornerColor)
            {
                return;
            }
        }

        transform.DOScale(Vector3.zero, 0.5f)
        .SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            PlaceGenerator.Instance.DropTilesAbove((int)transform.position.x, (int)transform.position.y);
            UiManager.Uinstance.Score += 10;
            Destroy(gameObject);
        });
    }
}