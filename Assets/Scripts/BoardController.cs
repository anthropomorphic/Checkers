using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
	public Piece SelectedPiece;
	
	private Tile[][] _tiles;

	public Tile GetTile(int rank, int file)
	{
		return _tiles[rank][file];
	}
	
	private void Start()
	{

		// Initialize `_ranks` and `_files` to 8x8 arrays
		_tiles = new Tile[8][];
		for (var i = 0; i < 8; i++)
		{
			_tiles[i] = new Tile[8];
		}

		// Get all the `Tile` objects and put them in `tiles`
		// These are not guaranteed to be in order
		// We need to sort them into a 2D array for simpler access
		var tiles = FindObjectsOfType<Tile>();
		
		// Insert each tile as an entry into `tileDict`
		var tileDict = new Dictionary<string, Tile>();
		foreach (var tile in tiles)
		{
			tileDict.Add(tile.name, tile);
		}
		
		// Populate `_tiles` in the appropriate order
		for (var i = 0; i < 8; i++)
		{
			for (var j = 0; j < 8; j++)
			{
				// Convert (0 -> 7) to ('1' -> '8')
				var n = (char) ((int) '1' + i);
				// Convert (0 -> 7) to ('A' -> 'H')
				var x = (char) ((int) 'A' + j);
				
				// Query `_tileDict` dictionary for tiles from `1A` to `8H`
				_tiles[i][j] = tileDict[$"{n}{x}"];
			}
		}
	}
}
