using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

	private Tile[][] _ranks;
	private Tile[][] _files;
	private Dictionary<string, Tile> _tileDict;
	
	private void Start()
	{

		// Initialize `_ranks` and `_files` to 8x8 arrays
		_ranks = new Tile[8][];
		_files = new Tile[8][];
		for (var i = 0; i < 8; i++)
		{
			_ranks[i] = new Tile[8];
			_files[i] = new Tile[8];
		}

		// Get all the `Tile` objects and put them in `tiles`
		var tiles = FindObjectsOfType<Tile>();
		
		// Insert each tile as an entry into `_tileDict`
		_tileDict = new Dictionary<string, Tile>();
		foreach (var tile in tiles)
		{
			_tileDict.Add(tile.name, tile);
		}
		
		// Populate `_ranks` and `_files` with tiles in thier respective orders
		for (var i = 0; i < 8; i++)
		{
			for (var j = 0; j < 8; j++)
			{
				// Convert (0 -> 7) to ('1' -> '8')
				var n = (char) ((int) '1' + i);
				// Convert (0 -> 7) to ('A' -> 'H')
				var x = (char) ((int) 'A' + j);
				
				// Query `_tileDict` dictionary for tiles from `1A` to `8H`
				_ranks[i][j] = _tileDict[$"{n}{x}"];
				_files[j][i] = _tileDict[$"{n}{x}"];
			}
		}
	}
}
