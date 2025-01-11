using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    [SerializeField] private PlaceGenerator _placeGenerator;

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
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.started && !waitClickTime)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            float closestColumnX = _placeGenerator.ClosestTilePosition(worldMousePosition.x);

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
    }
    IEnumerator waitNextClick()
    {
        waitClickTime = true;
        yield return new WaitForSeconds(1.5f);
        waitClickTime = false;
    }

    public void SetNextPiece()
    {
        _currentTile = null;
        TileBase randomTilePrefab = _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
        _currentTile = randomTilePrefab;
        _currentTile = Instantiate(randomTilePrefab, transform.position, Quaternion.identity);
    }

    public void GenerateTiles()
    {
        for (int i = 0; i < _desiredPositions.Count; i++)
        {
            Vector2 spawnPosition = _desiredPositions[i];
            TileBase randomTilePrefab = _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
            PlaceTileToGrid(spawnPosition, randomTilePrefab);
        }
    }

    public void PlaceTileToGrid(Vector2 tilePosition, TileBase tilePrefab)
    {
        _placeGenerator.PlaceTile(tilePosition, tilePrefab, false);
    } 
}