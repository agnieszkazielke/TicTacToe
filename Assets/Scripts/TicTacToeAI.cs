using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]


public class TicTacToeAI : MonoBehaviour
{

	// AI Instance
	public static TicTacToeAI Instance { get; private set; }

	// TicTacToe Buttons
	[SerializeField]
	private List<GameObject> buttons;

	[SerializeField]
	private GameObject buttonGroup;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;

	// Symbol settings for player & AI
	public TicTacToeState playerState = TicTacToeState.circle;
	public TicTacToeState aiState = TicTacToeState.cross;


	int noWinOpportunity = 999;
	
	[SerializeField]
	int _aiLevel;

	// Game bools

	[SerializeField]
	public bool _isPlayerTurn;
	[SerializeField]
	private bool winCheck;


	// UI references

	[SerializeField]
	private GameObject startPanel;

	[SerializeField]
	private GameObject gamePanel;


	private void Awake()
	{
		Instance = this;
		startPanel.SetActive(true);
		gamePanel.SetActive(false);
		buttonGroup.SetActive(false);

		
	}

	private void OnEnable()
    {
		EventManager.OnPlayerTurn += AllowPlayerMove;
		EventManager.OnPlayerCompleteMove += CheckBoard;
		EventManager.OnAITurn += AiMove;
		EventManager.OnAICompleteMove += CheckBoard;
		
    }


    private void OnDisable()
    {
		EventManager.OnPlayerTurn -= AllowPlayerMove;
		EventManager.OnPlayerCompleteMove -= CheckBoard;
		EventManager.OnAITurn -= AiMove;
		EventManager.OnAICompleteMove -= CheckBoard;
	}



	public void StartAI(int AILevel)
	{
		
		_aiLevel = AILevel;
		StartGame();
	}


	private void StartGame()
	{
		startPanel.SetActive(false);
		gamePanel.SetActive(true);
		buttonGroup.SetActive(true);
	}

	public void PlayerSelects(int coordX, int coordY){

		SetVisual(coordX, coordY, playerState);
	}

	public void AllowPlayerMove()
    {
		_isPlayerTurn = true;

	}



	private bool OneOfThreeTrue(bool a, bool b, bool c)
    {
		if (a || b || c)
        {
			return true;
        }

		else
        {
			return false;
        }
    }

	private bool TwoOfThreeTrue(bool a, bool b, bool c)
	{
		if ((a && b) || (b && c) || (a && c))
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	private List<bool> ButtonCircledFilter()
    {
		bool buttonCheck;

		List<bool> buttonsCircledFilter = new List<bool>();

		foreach (GameObject button in buttons)
		{
			if (button.GetComponent<ClickTrigger>().squareState == ClickTrigger.SquareState.circle)
			{
				buttonCheck = true;
			}

			else
			{
				buttonCheck = false;

			}


			buttonsCircledFilter.Add(buttonCheck);
		}

		return buttonsCircledFilter;
	}


	private List<bool> ButtonCrossedFilter()
	{
		bool buttonCheck;

		List<bool> buttonsCrossedFilter = new List<bool>();

		foreach (GameObject button in buttons)
		{
			if (button.GetComponent<ClickTrigger>().squareState == ClickTrigger.SquareState.cross)
			{
				buttonCheck = true;
			}

			else
			{
				buttonCheck = false;

			}


			buttonsCrossedFilter.Add(buttonCheck);
		}

		return buttonsCrossedFilter;
	}


	private List<bool> ButtonEmptyFilter()
	{
		bool buttonCheck;

		List<bool> buttonsEmptyFilter = new List<bool>();

		foreach (GameObject button in buttons)
		{
			if (button.GetComponent<ClickTrigger>().squareState == ClickTrigger.SquareState.empty)
			{
				buttonCheck = true;
			}

			else
			{
				buttonCheck = false;

			}


			buttonsEmptyFilter.Add(buttonCheck);
		}

		return buttonsEmptyFilter;
	}

	private int ValidateOpportunity(List<bool> filter, int a, int b, int c)
    {

		List<bool> buttonsEmptyFilter = ButtonEmptyFilter();

		

		if (TwoOfThreeTrue(filter[a], filter[b], filter[c])
		&& OneOfThreeTrue(buttonsEmptyFilter[a], buttonsEmptyFilter[b], buttonsEmptyFilter[c]))
		{
			

			if (buttonsEmptyFilter[a])
			{
				return a;
			}

			else if (buttonsEmptyFilter[b])
            {
				return b;
            }

			else 
            {
				return c;
            }

		}

		else
        {
            return noWinOpportunity;
        }
	}


	private int ValidateFutureOpportunity(List<bool> filter, int a, int b, int c)
	{
		List<bool> buttonsEmptyFilter = ButtonEmptyFilter();



		if (OneOfThreeTrue(filter[a], filter[b], filter[c])
		&& TwoOfThreeTrue(buttonsEmptyFilter[a], buttonsEmptyFilter[b], buttonsEmptyFilter[c]))
		{


			if (buttonsEmptyFilter[a])
			{
				return a;
			}

			else if (buttonsEmptyFilter[b])
			{
				return b;
			}

			else
			{
				return c;
			}

		}

		else
		{
			return noWinOpportunity;
		}
	}


	private int LookForTwoAIButtonAndOneFree()
	{
		

		if (ValidateOpportunity(ButtonCrossedFilter(),  0, 1, 2) != noWinOpportunity)
        {
			return ValidateOpportunity(ButtonCrossedFilter(), 0, 1, 2);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 3, 4, 5) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 3, 4, 5);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 6, 7, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 6, 7, 8);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 0, 3, 6) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 0, 3, 6);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 1, 4, 7) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 1, 4, 7);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 2, 5, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 2, 5, 8);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 0, 4, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 0, 4, 8);

		}

		else if (ValidateOpportunity(ButtonCrossedFilter(), 6, 4, 2) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCrossedFilter(), 6, 4, 2);

		}

		else
        {
			return noWinOpportunity;
        }


	}

	private int LookForOneAIButtonAndTwoFree()
	{


		if (ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 1, 2) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 1, 2);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 3, 4, 5) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 3, 4, 5);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 6, 7, 8) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 6, 7, 8);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 3, 6) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 3, 6);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 1, 4, 7) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 1, 4, 7);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 2, 5, 8) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 2, 5, 8);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 4, 8) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 0, 4, 8);

		}

		else if (ValidateFutureOpportunity(ButtonCrossedFilter(), 6, 4, 2) != noWinOpportunity)
		{
			return ValidateFutureOpportunity(ButtonCrossedFilter(), 6, 4, 2);

		}

		else
		{
			return noWinOpportunity;
		}


	}


	private int LookForPlayerNearWin()
	{
		if (ValidateOpportunity(ButtonCircledFilter(), 0, 1, 2) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 0, 1, 2);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 3, 4, 5) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 3, 4, 5);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 6, 7, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 6, 7, 8);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 0, 3, 6) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 0, 3, 6);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 1, 4, 7) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 1, 4, 7);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 2, 5, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 2, 5, 8);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 0, 4, 8) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 0, 4, 8);

		}

		else if (ValidateOpportunity(ButtonCircledFilter(), 6, 4, 2) != noWinOpportunity)
		{
			return ValidateOpportunity(ButtonCircledFilter(), 6, 4, 2);

		}

		else
		{
			return noWinOpportunity;
		}
	}



	private GameObject PickRandomButton()
	{
		List<GameObject> freeButtons = new List<GameObject>();

		GameObject selectedButton;


		foreach (GameObject button in buttons)
		{
			if (button.GetComponent<ClickTrigger>().canClick == true)
			{
				freeButtons.Add(button);
			}
		}

		int index = Random.Range(0, freeButtons.Count);


		selectedButton = freeButtons[index];

		return selectedButton;
	}



	private bool CheckButtonsForWin(List<bool> filter)
    {

		// Configuration 1: Top horizontal 3: Circles

		if (filter[0] && filter[1] && filter[2])
		{
			winCheck = true;
			return winCheck;
        }


		// Configuration 2: Middle horizontal 3: Circles

		else if (filter[3] && filter[4] && filter[5])
		{
			winCheck = true;
			return winCheck;
		}

		// Configuration 3: Bottom horizontal 3: Circles

		else if (filter[6] && filter[7] && filter[8])
		{
			winCheck = true;
			return winCheck;
		}


		// Configuration 4: Left vertical 3: Circles

		else if (filter[0] && filter[3] && filter[6])
		{
			winCheck = true;
			return winCheck;
		}


		// Configuration 5: Centre vertical 3: Circles

		else if (filter[1] && filter[4] && filter[7] )
		{
			winCheck = true;
			return winCheck;
		}


		// Configuration 6: Right vertical 3: Circles

		else if (filter[2] && filter[5] && filter[8])
		{
			winCheck = true;
			return winCheck;
		}

		// Configuration 7: Across from Top Left to Bottom Right: Circles

		else if (filter[0] && filter[4] && filter[8])
		{
			winCheck = true;
			return winCheck;
		}

		// Configuration 8A: Across from Bottom Left to Top Right: Circles

		else if (filter[6] && filter[4]  && filter[2])
		{
			winCheck = true;
			return winCheck;
		}

		else
        {
			winCheck = false;
			return winCheck;
        }

	}

	private bool CheckBoardFull()
    {
		bool boardFull = false;


		foreach (GameObject button in buttons)
        {
			if(button.GetComponent<ClickTrigger>().canClick)
            {
				boardFull = false;
				
				break;
			
            }

            else
            {
				boardFull = true;
            }

        }

		return boardFull;

	}


	private void CheckBoard()
    {
		

		// check if player won	

		if (CheckButtonsForWin(ButtonCircledFilter()))
        {
			_isPlayerTurn = false;
			EventManager.PlayerWin();
			Debug.Log("Player won!");
        }

		// check if AI won

		else if (CheckButtonsForWin(ButtonCrossedFilter()))
        {
			_isPlayerTurn = false;
			EventManager.AIWin();
			Debug.Log("AI won!");
		}

		// check if there is a tie

		else if (CheckBoardFull())
        {
			_isPlayerTurn = false;
			EventManager.Draw();
			Debug.Log("It's a draw");
		}

		else

        {
			if (_isPlayerTurn == true)
            {
				_isPlayerTurn = false;
				EventManager.AITurn();
			}

			else
            {
				EventManager.PlayerTurn();
			}
			
		}
		

	}

	public void AiMove()
    {
		int noWinOpportunity = 999;
		GameObject selectedButton;

		// Check if AI can win in this round

		// EASY LEVEL

		if (_aiLevel == 0)
		{
			if (LookForTwoAIButtonAndOneFree() != noWinOpportunity)

			{
				selectedButton = buttons[LookForTwoAIButtonAndOneFree()];
			}


			// Pick middle central button if available

			else if (buttons[4].GetComponent<ClickTrigger>().squareState == ClickTrigger.SquareState.empty)
			{
				selectedButton = buttons[4];
			}

			// Pick Random Button

			else

			{
				selectedButton = PickRandomButton();
			}
		}

		// HARD LEVEL
		else
        {
			if (LookForTwoAIButtonAndOneFree() != noWinOpportunity)

			{
				selectedButton = buttons[LookForTwoAIButtonAndOneFree()];
			}

			// Check if player can win in next round and, if so, stop it from happening

			else if (LookForPlayerNearWin() != noWinOpportunity)
			{
				selectedButton = buttons[LookForPlayerNearWin()];

			}

			// Pick middle central button if available

			else if (buttons[4].GetComponent<ClickTrigger>().squareState == ClickTrigger.SquareState.empty)
			{
				selectedButton = buttons[4];
			}

			// Pick a button in a row of one taken by AI and two free

			else if (LookForOneAIButtonAndTwoFree() != noWinOpportunity)
            {
				selectedButton = buttons[LookForOneAIButtonAndTwoFree()];
            }

			// Pick Random Button

			else

			{
				selectedButton = PickRandomButton();
			}
		}

				


		int coordX = selectedButton.GetComponent<ClickTrigger>()._myCoordX;
		int coordY = selectedButton.GetComponent<ClickTrigger>()._myCoordY;


		// Selection completed in the system

		selectedButton.GetComponent<ClickTrigger>().SquareUsed();

		selectedButton.GetComponent<ClickTrigger>().CrossSquare();


		StartCoroutine(AiSelects(coordX, coordY));


    }


	public IEnumerator AiSelects(int coordX, int coordY)
    {

		yield return new WaitForSeconds(1.0f);
		
		SetVisual(coordX, coordY, aiState);

		EventManager.AICompleteMove();


	}


	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		if (targetState == TicTacToeState.circle)
        {
			Instantiate(
				_oPrefab, new Vector3(coordX, 0, coordY), Quaternion.identity);  
        }

		if (targetState == TicTacToeState.cross)
		{
			Instantiate(
				_xPrefab, new Vector3(coordX, 0, coordY), Quaternion.identity);
		}


	}



}
