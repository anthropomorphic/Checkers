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
        var selectedPiece = _board.SelectedPiece;
        
        if (selectedPiece == null) return;
        
        // If this tile already has an occupant, ignore the click
        if (Occupant != null) return;
        
        // TODO: Check if this piece is a king (can move backward)
        
        if (IsJumpableFrom(selectedPiece))
        {
            var jumpedPiece = GetJumpedOverTile(selectedPiece).Occupant;
            
            if (jumpedPiece == null) return;
            
            if (jumpedPiece.Color == selectedPiece.Color) return;
            
            jumpedPiece.Capture();
            
            selectedPiece.MoveTo(Rank, File, transform.position);
            
            if (!selectedPiece.CanJump())
            {
                _board.EndTurn();
            }
        }
        else if (IsMovableFrom(selectedPiece))
        {
            selectedPiece.MoveTo(Rank, File, transform.position);
            
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
