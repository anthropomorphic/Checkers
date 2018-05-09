using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private CheckersController _checkers;

    public Piece Occupant;
    
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
        
        // If this piece already has an occupant, ignore the click
        if (Occupant != null) return;
        
        // TODO: Check if this piece is a king (can move backward)
        
        // Check if this tile can be jumped to
        if (IsJumpableFrom(_checkers.SelectedPiece))
        {
            // If there is no piece on the intervening tile, ignore the click
            // TODO: Discriminate against who's piece it is
            var jumpedOverTile = GetJumpedOverTile(_checkers.SelectedPiece);
            if (jumpedOverTile.Occupant == null) return;
            
            // Otherwise, capture it
            jumpedOverTile.Occupant.Capture();
        }
        
        // Move the piece to this tile
        _checkers.SelectedPiece.MoveTo(Rank, File, transform.position);
        
        // Deselect the piece we just moved
        _checkers.SelectedPiece = null;
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

    /**
     * Returns the Tile that resides between this tile and the one that `piece` occupies
     *
     * Returns null if `IsJumpableFrom(piece) == false`
     */
    private Tile GetJumpedOverTile(Piece piece)
    {
        if (!IsJumpableFrom(piece)) return null;
        
        // Find the offset in rank and file of the piece with respect to this tile
        // (They will be either +2 or -2, as verified by `IsJumpableFrom()`)
        var rankOffset = piece.Rank - Rank;
        var fileOffset = piece.File - File;
        
        // Decrease the magnitude of the offsets to 1
        {
            var rankChange = rankOffset < 0 ? 1 : -1;
            var fileChange = fileOffset < 0 ? 1 : -1;
            rankOffset += rankChange;
            fileOffset += fileChange;
        }
        // Get the tile and return it
        return _checkers.GetTile(Rank + rankOffset, File + fileOffset);
    }
}
