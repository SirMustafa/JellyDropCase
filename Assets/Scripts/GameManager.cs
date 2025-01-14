using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    [SerializeField] private PlaceGenerator _placeGenerator;
    [SerializeField] Inputs inputs;
    [SerializeField] private List<TileBase> _tilePrefabs;
    [SerializeField] private List<Vector2> _desiredPositions = new List<Vector2>();

    TileBase _currentTile;
    bool waitClickTime = false;

    private void Awake()
    {
        GameManagerInstance = this;
    }

    private void Start()
    {
        _placeGenerator.GeneratePlaces();
        GenerateTiles();
        SetNextPiece();
    }
    public void CreateTile(float closestColumnX)
    {        
        Vector2? emptyTilePosition = _placeGenerator.FindEmptyTilePositionInColumn(closestColumnX);

        if (emptyTilePosition.HasValue)
        {
            _placeGenerator.PlaceTile(emptyTilePosition.Value, _currentTile, true);
            SetNextPiece();
        }
        else
        {
            Debug.Log("Bu sütunda boþ pozisyon yok.");
            return;
        }
    }

    public void SetNextPiece()
    {
        _currentTile = null;
        TileBase randomTilePrefab = _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
        _currentTile = randomTilePrefab;
        _currentTile = Instantiate(randomTilePrefab, transform.position, Quaternion.identity);
        _currentTile.transform.SetParent(this.transform);
        inputs.GetNextPiece(_currentTile);
    }

    public void GenerateTiles()
    {
        for (int i = 0; i < _desiredPositions.Count; i++)
        {
            Vector2 spawnPosition = _desiredPositions[i];
            TileBase randomTilePrefab = _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
            _placeGenerator.PlaceTile(spawnPosition, randomTilePrefab, false);
        }
    }
    public void SetMouseClickTime()
    {
        waitClickTime = !waitClickTime;
    }
}