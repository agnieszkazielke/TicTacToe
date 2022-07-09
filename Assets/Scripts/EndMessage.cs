using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMessage : MonoBehaviour
{

	[SerializeField]
	private TMP_Text _playerMessage = null;

	private void OnEnable()
	{
		EventManager.OnPlayerWin += PlayerWinsMessage;
		EventManager.OnAIWin += AIWinsMessage;
		EventManager.OnDraw += DrawMessage;
	}


	private void OnDisable()
	{
		EventManager.OnPlayerWin -= PlayerWinsMessage;
		EventManager.OnAIWin -= AIWinsMessage;
		EventManager.OnDraw -= DrawMessage;
	}

	private void PlayerWinsMessage()
    {
		_playerMessage.text = "Player wins";
	}

	private void AIWinsMessage()
	{
		_playerMessage.text = "AI wins";
	}

	private void DrawMessage()
	{
		_playerMessage.text = "It's a draw";
	}



}
