using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private CheckersController _checkers;
    
    // Set in editor
    public Tile StartingTile;

    public int Rank { get; private set; }
    public int File { get; private set; }

    private void Start()
    {
        StartCoroutine(NonBlockingStart());
    }
    
    /**
     * Since this piece relies on the tile it resides on, it must wait for tile to be
     * initialized first with this non-blocking coroutine
     */
    private IEnumerator NonBlockingStart()
    {
        _checkers = GameObject.FindObjectOfType<CheckersController>();
        
        // Wait for `StartingTile` to be initialized
        while (!StartingTile.IsInitialized)
        {
            yield return new WaitForSeconds(0.01f);
        }

        Rank = StartingTile.Rank;
        File = StartingTile.File;
    }
    
    private void OnMouseUp()
    {
        _checkers.SelectedPiece = this;
    }

    public void MoveTo(int rank, int file, Vector3 pos)
    {
        Rank = rank;
        File = file;
        transform.position = pos;
    }
}
