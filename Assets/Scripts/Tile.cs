using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private ChessController _chess;

    private Piece _occupant;

    public bool IsInitialized { get; private set; }
    public int Rank { get; private set; }
    public int File { get; private set; }
    
    private void Start()
    {
        _chess = GameObject.FindObjectOfType<ChessController>();
        
        // Get rank and file from name ("3B" -> (3, 2))
        Rank = (int) name[0] - '1';
        File = (int) name[1] - 'A';

        IsInitialized = true;
    }

    private void OnMouseUp()
    {
        
        // If no piece is selected, ignore the click
        if (_chess.SelectedPiece == null) return;
        
        // Check if this tile can be moved to (not jumped to)
        if (IsMovableFrom(_chess.SelectedPiece))
        {
            // TODO: Check if there is a piece already there
            
            // TODO: Check if this piece is a king (can move backward)
            
            _chess.SelectedPiece.MoveTo(Rank, File, transform.position);
        }
    }
    
    private bool IsMovableFrom(Piece piece)
    {
        return Math.Abs(Rank - piece.Rank) == 1 && Math.Abs(File - piece.File) == 1;
    }
}
