using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{

	
	public enum SquareState { empty, cross, circle }

	// location of the centre square 
	public int _myCoordX { get; private set; }
	public int _myCoordY { get; private set; }

	public SquareState squareState { get; private set; }

	[SerializeField]
	public bool canClick { get; private set; }

	private void Awake()
	{
		_myCoordX = (int)transform.position.x;
		_myCoordY = (int)transform.position.z;
	}

	private void Start(){


		canClick = true;
		squareState = SquareState.empty;
	}



	private void OnMouseDown()
	{
		if(canClick && TicTacToeAI.Instance._isPlayerTurn == true)
		
		{
			TicTacToeAI.Instance.PlayerSelects(_myCoordX, _myCoordY);

			SquareUsed();
			CircleSquare();

			EventManager.PlayerCompleteMove();
						
		}
				

	}

	private void CircleSquare()
    {
		squareState = SquareState.circle;
    }

	public void CrossSquare()
    {
		squareState = SquareState.cross;
    }


	public void SquareUsed()
    {
		canClick = false;
    }



}
