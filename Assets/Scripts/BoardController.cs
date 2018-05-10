using UnityEngine;
using PlayerColor = Piece.PlayerColor;


public class BoardController : MonoBehaviour
{
	public PlayerColor PlayerTurn { get; private set; }
	public Piece SelectedPiece;
	
	// A 2D array of tiles, accessed by `_tiles[rank][file]`
	private Tile[][] _tiles;
	private TurnIndicator _turnIndicator;

	public Tile GetTile(int rank, int file)
	{
		return _tiles[rank][file];
	}

	private void Awake()
	{
		_tiles = new Tile[8][];
		for (var i = 0; i < 8; i++)
		{
			_tiles[i] = new Tile[8];
		}

		_turnIndicator = GameObject.FindObjectOfType<TurnIndicator>();

		PlayerTurn = PlayerColor.Black;
	}
	
	private void Start()
	{
		// Get all the `Tile` objects and order them by rank and file
		var tiles = FindObjectsOfType<Tile>();

		foreach (var tile in tiles)
		{
			_tiles[tile.Rank][tile.File] = tile;
		}
	}

	public void EndTurn()
	{
		// Switch player turn (Red -> Black | Black -> Red)
		PlayerTurn = (PlayerTurn == PlayerColor.Black) ? PlayerColor.Red : PlayerColor.Black;
		
		_turnIndicator.UpdateTurn();
		
		// Deselect the selected piece
		SelectedPiece = null;
	}
}
