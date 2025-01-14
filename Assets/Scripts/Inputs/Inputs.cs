using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] PlaceGenerator placeGenerator;
    [SerializeField] float mouseSensitivity;
    public GameObject currentTile;
    private bool _isDragging = false;
    private Vector2 _startMousePosition;
    private Vector2 _startTilePosition;
    float newX;

    private void Start()
    {
        placeGenerator = PlaceGenerator.Instance;
    }
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isDragging = true;
            _startMousePosition = Mouse.current.position.ReadValue();
            _startTilePosition = currentTile.transform.position;
        }
        else if (context.canceled)
        {
            _isDragging = false;
            OnTileReleased();
        }
    }

    public void OnSwipe(InputAction.CallbackContext context)
    {
        if (_isDragging && context.performed)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            float deltaX = (currentMousePosition.x - _startMousePosition.x) * mouseSensitivity;
            newX = _startTilePosition.x + deltaX;
            newX = Mathf.Clamp(newX, 0, 5f);
            currentTile.transform.position = new Vector2(newX, _startTilePosition.y);
        }
    }
    private void OnTileReleased()
    {
        float closestColumnX = placeGenerator.ClosestTilePosition(newX);
        currentTile.transform.DOMoveX(closestColumnX, 0.5f).OnComplete(() =>
        {
            GameManager.GameManagerInstance.CreateTile(closestColumnX);  
        });
    }
    public void GetNextPiece(TileBase _currentile)
    {
        currentTile = _currentile.gameObject;
    }
}