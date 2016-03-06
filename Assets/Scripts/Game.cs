using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class Game : MonoBehaviour
{

	public int linesCount;
	[Serializable] public class LinesCountChangedEventClass : UnityEvent<int> {}
	public LinesCountChangedEventClass LinesCountChangedEvent;


	void Start ()
	{
		linesCount = 0;
	}


	public void OnFullLineFound(int y)
	{
		linesCount++;
		LinesCountChangedEvent.Invoke (linesCount);
	}


}

