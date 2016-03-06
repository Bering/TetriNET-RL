using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StatsView : MonoBehaviour {

	public Text linesCount;


	public void OnLinesCountChanged(int newCount)
	{
		linesCount.text = newCount.ToString ();
	}

}
