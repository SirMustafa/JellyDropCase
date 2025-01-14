using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolder : MonoBehaviour
{
    private Vector2 _myPosition;
    public TileBase CurrentTile;

    private void OnEnable()
    {
        _myPosition = transform.position;
    }

    public void Initialize(TileBase myTile)
    {
        CurrentTile = myTile;
        CurrentTile.transform.position = _myPosition;
    }

    public TileBase GetCurrentTile()
    {
        return CurrentTile;
    }
}