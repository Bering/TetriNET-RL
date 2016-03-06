using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics.SymbolStore;


public class UILabelUpdater : MonoBehaviour {

	protected Text label;

	void Awake()
	{
		label = GetComponent<Text> ();
	}


	public void UpdateLabelInt(int newValue)
	{
		label.text = newValue.ToString();
	}

	public void UpdateLabelFloat(float newValue)
	{
		label.text = newValue.ToString();
	}

	public void UpdateLabelString(string newValue)
	{
		label.text = newValue;
	}


	public void AppendInt(int valueToAdd)
	{
		label.text += valueToAdd.ToString();
	}

	public void AppendFloat(float valueToAdd)
	{
		label.text += valueToAdd.ToString();
	}

	public void AppendString(string valueToAdd)
	{
		label.text += valueToAdd;
	}
}
