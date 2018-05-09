using UnityEngine;

public class BoardController : MonoBehaviour
{
	public Piece SelectedPiece;
	
	// A 2D array of tiles, accessed by `_tiles[rank][file]`
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

		// Get all the `Tile` objects
		// These are not guaranteed to be in order
		// We need to sort them into a 2D array for simpler access
		var tiles = FindObjectsOfType<Tile>();

		foreach (var tile in tiles)
		{
			// Convert ('1' -> '8') to (0 -> 7)
			var rank = (int) tile.name[0] - '1';
			// Convert ('A' -> 'H') to (0 -> 7)
			var file = (int) tile.name[1] - 'A';

			_tiles[rank][file] = tile;
		}
	}
}
