using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
	protected ActivityLog activityLog = null;

	public int linesCount;

	public UnityEvent GameStartedEvent;
	public UnityEvent GamePausedEvent;
	public UnityEvent GameResumedEvent;
	public GameOverEventClass GameOverEvent; [Serializable] public class GameOverEventClass : UnityEvent<bool> {}
	public LinesCountChangedEventClass LinesCountChangedEvent; [Serializable] public class LinesCountChangedEventClass : UnityEvent<int> {}


	void Awake()
	{
		Services.Register<GameManager> (this);
	}


	void Start ()
	{
		StartGame ();
	}


	public void ResetGame()
	{
		linesCount = 0;
	}


	public void StartGame()
	{
		activityLog = Services.Find<ActivityLog> ();

		ResetGame ();

		activityLog.Append("System", "Game started");
		GameStartedEvent.Invoke();
	}


	public void OnFullLineFound(int y)
	{
		linesCount++;
		LinesCountChangedEvent.Invoke (linesCount);
	}


}

