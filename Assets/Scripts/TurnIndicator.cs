using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
	private BoardController _board;
	private Text _turnIndicatorMessage;
	
	private void Awake ()
	{
		_board = GameObject.FindObjectOfType<BoardController>();
		_turnIndicatorMessage = transform.GetChild(0).GetComponent<Text>();

		UpdateTurn();
	}

	public void UpdateTurn()
	{
		_turnIndicatorMessage.text = $"{_board.PlayerTurn.ToString()}'s Turn";
	}

	public void PlayerWon(Piece.PlayerColor player)
	{
		_turnIndicatorMessage.text = $"{player.ToString()} Won!";
	}
}
