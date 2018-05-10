using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private BoardController _board;
    
    // Set in editor
    public Tile StartingTile;
    public PlayerColor Color;
    
    public enum PlayerColor
    {
        Black, Red
    }
    
    public int Rank { get; private set; }
    public int File { get; private set; }

    private void Awake()
    {
        _board = GameObject.FindObjectOfType<BoardController>();
    }
    
    private void Start()
    {
        Rank = StartingTile.Rank;
        File = StartingTile.File;
    }
    
    private void OnMouseUp()
    {
        // If the piece is the wrong color (not their turn), ignore the click
        if (_board.PlayerTurn != Color) return;
        
        // If a player has already won, ignore the click
        if (_board.PlayerWon) return;
        
        _board.SelectedPiece = this;
    }

    /**
     * Moves the piece to a new tile, given by its rank, file, and position
     *
     * Also unregisters with the tile it leaves and
     * registers with the tile it moves to
     */
    public void MoveTo(int rank, int file, Vector3 pos)
    {
        _board.GetTile(Rank, File).Occupant = null;
        Rank = rank;
        File = file;
        transform.position = pos;
        _board.GetTile(Rank, File).Occupant = this;
    }

    /**
     * Called when this piece gets captured
     */
    public void Capture()
    {
        // TODO: Move off to the side of the board
        // For now, we'll just destroy the piece
        Destroy(gameObject);
        _board.GetTile(Rank, File).Occupant = null;
        _board.CapturePiece(Color);
    }

    public bool CanJump()
    {
        if (Rank > 1 && File > 1)
        {
            if (_board.GetTile(Rank - 2, File - 2).Occupant == null &&
                _board.GetTile(Rank - 1, File - 1).Occupant != null) return true;
        }

        if (Rank > 1 && File < 6)
        {
            if (_board.GetTile(Rank - 2, File + 2).Occupant == null &&
                _board.GetTile(Rank - 1, File + 1).Occupant != null) return true;
        }

        if (Rank < 6 && File > 1)
        {
            if (_board.GetTile(Rank + 2, File - 2).Occupant == null &&
                _board.GetTile(Rank + 1, File - 1).Occupant != null) return true;
        }

        if (Rank < 6 && File < 6)
        {
            if (_board.GetTile(Rank + 2, File + 2).Occupant == null &&
                _board.GetTile(Rank + 1, File + 1).Occupant != null) return true;
        }
        return false;
    }
}
