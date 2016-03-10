using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics.SymbolStore;


public class UILabelUpdater : MonoBehaviour {

	protected Text label;

	void Awake()
	{
		label = GetComponent<Text> ();
	}


	public void UpdateLabel(int newValue)
	{
		label.text = newValue.ToString();
	}

	public void UpdateLabel(float newValue)
	{
		label.text = newValue.ToString();
	}

	public void UpdateLabel(string newValue)
	{
		label.text = newValue;
	}


	public void AppendToLabel(int valueToAdd)
	{
		label.text += valueToAdd.ToString();
	}

	public void AppendToLabel(float valueToAdd)
	{
		label.text += valueToAdd.ToString();
	}

	public void AppendToLabel(string valueToAdd)
	{
		label.text += valueToAdd;
	}
}
