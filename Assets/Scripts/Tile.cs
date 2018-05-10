using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BoardController _board;

    public Piece Occupant;
    public int Rank { get; private set; }
    public int File { get; private set; }
    
    private void Awake()
    {
        _board = GameObject.FindObjectOfType<BoardController>();
        
        // Get rank and file from name ("3B" -> (3, 2))
        Rank = (int) name[0] - '1';
        File = (int) name[1] - 'A';
    }

    private void OnMouseUp()
    {
        // If no piece is selected, ignore the click
        if (_board.SelectedPiece == null) return;
        
        // If this piece already has an occupant, ignore the click
        if (Occupant != null) return;
        
        // TODO: Check if this piece is a king (can move backward)

        var didJump = false;
        
        // Check if this tile can be jumped to
        if (IsJumpableFrom(_board.SelectedPiece))
        {
            // Get the piece on the intervening tile (if there is one)
            var jumpedPiece = GetJumpedOverTile(_board.SelectedPiece).Occupant;
            
            // If there is no piece on the intervening tile, ignore the click
            if (jumpedPiece == null) return;
            
            // If the jumped piece is of the same color as the selected piece, ignore the click
            if (jumpedPiece.Color == _board.SelectedPiece.Color) return;
            
            // Otherwise, capture it
            jumpedPiece.Capture();
            
            didJump = true;
        }
        
        // Move the piece to this tile
        _board.SelectedPiece.MoveTo(Rank, File, transform.position);

        // End the turn if there is no possible double jump
        if (!didJump || !_board.SelectedPiece.CanJump())
        {
            _board.EndTurn();
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
        return _board.GetTile(Rank + rankOffset, File + fileOffset);
    }
}
