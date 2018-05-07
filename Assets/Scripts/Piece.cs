using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Set in editor
    public Tile StartingTile;

    private Tile _currentTile;

    private void Start()
    {
        _currentTile = StartingTile;
    }
}
