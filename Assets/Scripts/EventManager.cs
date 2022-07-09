using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void GameEvent();

    public delegate IEnumerator QuitGameAction();

    // Event: Player's Turn
    public static event GameEvent OnPlayerTurn;

    // Event: Player completed move
    public static event GameEvent OnPlayerCompleteMove;

    // Event: AI's Turn
    public static event GameEvent OnAITurn;

    // Event: AI completed move
    public static event GameEvent OnAICompleteMove;

    // Event: Player won
    public static event GameEvent OnPlayerWin;

    // Event: AI won
    public static event GameEvent OnAIWin;

    // Event: Draw
    public static event GameEvent OnDraw;

    // Event: Quit Game
    public static event QuitGameAction OnQuitGame;

    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void PlayerTurn()
    {
        if (OnPlayerTurn != null)
        {
            OnPlayerTurn();
        }
    }

    public static void PlayerCompleteMove()
    {
        if (OnPlayerCompleteMove != null)
        {
            OnPlayerCompleteMove();
        }
    }


    public static void AITurn()
    {
        if (OnAITurn != null)
        {
            OnAITurn();
        }
    }

    public static void AICompleteMove()
    {
        if (OnAICompleteMove != null)
        {
            OnAICompleteMove();
        }
    }


    public static void PlayerWin()
    {
        if (OnPlayerWin != null)
        {
            OnPlayerWin();
        }
    }

    public static void AIWin()
    {
        if (OnAIWin != null)
        {
            OnAIWin();
        }
    }

    public static void Draw()
    {
        if (OnDraw != null)
        {
            OnDraw();
        }
    }

    public void QuitGame()
    {
        if (OnQuitGame != null)
        {
            StartCoroutine(OnQuitGame());
        }
    }
}
