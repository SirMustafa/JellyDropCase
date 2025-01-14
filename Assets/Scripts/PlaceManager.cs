using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceManager : MonoBehaviour
{
    public static PlaceManager instance;
    [SerializeField] private GameObject _placePrefab;
    [SerializeField] private int _width, _height;

    private List<float> _groundPositionsX = new List<float>();
    private Dictionary<Vector2, TileBase> _tileDictionary = new Dictionary<Vector2, TileBase>();

    private Vector2[] neighborOffsets = new Vector2[]
    {
        new Vector2(-1, 0),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(0, 1)
    };

    private Dictionary<Vector2, (int, int)[]> offsetColorMap = new Dictionary<Vector2, (int, int)[]>
    {
        { Vector2.up, new (int, int)[] { (1, 3), (0, 2) } },
        { Vector2.left, new (int, int)[] { (0, 1), (2, 3) } },
        { Vector2.down, new (int, int)[] { (2, 0), (3, 1) } },
        { Vector2.right, new (int, int)[] { (3, 2), (1, 0) } }
    };
    private void Awake()
    {
        instance = this;
    }
    public void GeneratePlaces()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (y == 0)
                {
                    _groundPositionsX.Add(x);
                }

                Vector2 spawnPosition = new Vector2(x, y);
                GameObject spawnedPrefab = Instantiate(_placePrefab, spawnPosition, Quaternion.identity);
                spawnedPrefab.transform.SetParent(this.transform);
                spawnedPrefab.name = $"Place{x}_{y}";
                _tileDictionary.Add(spawnPosition, null);
            }
        }
    }

    public void PlaceTile(Vector2 tilePosition, TileBase tilePrefab, bool isNextPiece)
    {
        if (!_tileDictionary.ContainsKey(tilePosition))
        {
            Debug.LogError($"Hata: {tilePosition} pozisyonu geçersiz.");
            return;
        }

        if (_tileDictionary[tilePosition] != null)
        {
            Debug.LogError($"Hata: {tilePosition} konumunda zaten bir Tile var.");
            return;
        }

        TileBase placedTile;

        if (isNextPiece)
        {
            placedTile = tilePrefab;
            placedTile.transform.DOMove(tilePosition, 1).OnComplete(() =>
            {
                placedTile.name = $"Tile_{tilePosition.x}_{tilePosition.y}";
                _tileDictionary[tilePosition] = placedTile;
                CheckCorners(tilePosition, placedTile);
            });
        }
        else
        {
            placedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            placedTile.transform.SetParent(this.transform);
            placedTile.name = $"Tile_{tilePosition.x}_{tilePosition.y}";
            _tileDictionary[tilePosition] = placedTile;
            CheckCorners(tilePosition, placedTile);
        }
    }

    private void CheckCorners(Vector2 tilePosition, TileBase placedTile)
    {
        foreach (Vector2 offset in neighborOffsets)
        {
            Vector2 neighborPosition = tilePosition + offset;

            if (_tileDictionary.TryGetValue(neighborPosition, out TileBase neighborTile) && neighborTile != null)
            {
                foreach ((int currentTileIndex, int neighborTileIndex) in offsetColorMap[offset])
                {
                    Color placedTileColor = placedTile.GetChildColor(currentTileIndex);
                    Color neighborTileColor = neighborTile.GetChildColor(neighborTileIndex);

                    if (placedTileColor == neighborTileColor)
                    {
                        HandleMatchingColors(placedTile, neighborTile, currentTileIndex, neighborTileIndex);
                    }
                }
            }
        }
    }

    private void HandleMatchingColors(TileBase tile1, TileBase tile2, int tile1Index, int tile2Index)
    {
        Color matchedColor = tile1.GetChildColor(tile1Index);
        ReplaceMatchingColors(tile1, matchedColor);
        ReplaceMatchingColors(tile2, matchedColor);

        tile1.UpdateVisuals();
        tile2.UpdateVisuals();
    }

    private void ReplaceMatchingColors(TileBase tile, Color matchedColor)
    {
        for (int i = 0; i < 4; i++)
        {
            if (tile.GetChildColor(i) == matchedColor)
            {
                int neighborIndex1 = GetDirectNeighborIndex(i, true);
                int neighborIndex2 = GetDirectNeighborIndex(i, false);

                Color neighborColor1 = tile.GetChildColor(neighborIndex1);
                Color neighborColor2 = tile.GetChildColor(neighborIndex2);

                if (neighborColor1 != matchedColor)
                {
                    tile.SetChildColor(i, neighborColor1);
                }
                else if (neighborColor2 != matchedColor)
                {
                    tile.SetChildColor(i, neighborColor2);
                }
            }
        }
    }

    private int GetDirectNeighborIndex(int cornerIndex, bool isFirstNeighbor)
    {
        switch (cornerIndex)
        {
            case 0: return isFirstNeighbor ? 1 : 2;
            case 1: return isFirstNeighbor ? 0 : 3;
            case 2: return isFirstNeighbor ? 0 : 3;
            case 3: return isFirstNeighbor ? 1 : 2;
            default: return -1;
        }
    }

    public Vector2? FindEmptyTilePositionInColumn(float columnX)
    {
        for (int y = 0; y < _height; y++)
        {
            Vector2 position = new Vector2(columnX, y);
            if (_tileDictionary[position] == null)
            {
                return position;
            }
        }
        return null;
    }

    public float ClosestTilePosition(float mouseXposition)
    {
        float closestXPosition = _groundPositionsX[0];
        float smallestDifference = Mathf.Abs(mouseXposition - closestXPosition);

        foreach (float xPos in _groundPositionsX)
        {
            float difference = Mathf.Abs(mouseXposition - xPos);

            if (difference < smallestDifference)
            {
                smallestDifference = difference;
                closestXPosition = xPos;
            }
        }
        return closestXPosition;
    }


    public void DropTilesAbove(int xPos, int yPos)
    {
        Vector2 temp = new Vector2(xPos, yPos);
        _tileDictionary[temp] = null;

        for (int y = yPos + 1; y < _height; y++)
        {
            Vector2 currentPosition = new Vector2(xPos, y);
            Vector2 newPosition = new Vector2(xPos, y - 1);

            if (newPosition.y < 0)
            {
                continue;
            }

            if (_tileDictionary.TryGetValue(currentPosition, out TileBase currentTile) &&
                _tileDictionary.TryGetValue(newPosition, out TileBase newTile) &&
                currentTile != null)
            {
                currentTile.transform.DOMove(newPosition, 0.5f).OnComplete(() =>
                {
                    _tileDictionary[newPosition] = currentTile;
                    _tileDictionary[currentPosition] = null;
                    CheckCorners(newPosition, currentTile);
                });
            }
        }
    }
}