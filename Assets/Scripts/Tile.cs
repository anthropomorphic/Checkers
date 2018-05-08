using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private CheckersController _checkers;

    private Piece _occupant;

    public bool IsInitialized { get; private set; }
    public int Rank { get; private set; }
    public int File { get; private set; }
    
    private void Start()
    {
        _checkers = GameObject.FindObjectOfType<CheckersController>();
        
        // Get rank and file from name ("3B" -> (3, 2))
        Rank = (int) name[0] - '1';
        File = (int) name[1] - 'A';

        IsInitialized = true;
    }

    private void OnMouseUp()
    {
        
        // If no piece is selected, ignore the click
        if (_checkers.SelectedPiece == null) return;
        
        // Check if this tile can be moved to (not jumped to)
        if (IsMovableFrom(_checkers.SelectedPiece))
        {
            // TODO: Check if there is a piece already there
            
            // TODO: Check if this piece is a king (can move backward)
            
            _checkers.SelectedPiece.MoveTo(Rank, File, transform.position);
        }
        // If not, check if this tile can be jumped to
        else if (IsJumpableFrom(_checkers.SelectedPiece))
        {
            // TODO: Check if there is a piece already there
            
            // TODO: Check if this piece is a king (can move backward)
            
            // TODO: Check if there is an oponents piece on the intervening tile
            
            _checkers.SelectedPiece.MoveTo(Rank, File, transform.position);
        }
    }
    
    /**
     * Checks if a piece lies on a tile which is diagonally adjacent to this one
     */
    private bool IsMovableFrom(Piece piece)
    {
        // Return true if the ranks and files of the two tiles are different by exactly 1
        return Math.Abs(Rank - piece.Rank) == 1 && Math.Abs(File - piece.File) == 1;
    }

    /**
     * Checks if a piece lies on a tile diagonal to this one, with one intervening tile
     */
    private bool IsJumpableFrom(Piece piece)
    {
        // Return true if the ranks and files of the two tiles are different by exactly 2
        return Math.Abs(Rank - piece.Rank) == 2 && Math.Abs(File - piece.File) == 2;
    }
}
