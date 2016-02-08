using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour
{
	public const int Width = 12;
	public const int Height = 12;

	static protected int[] grid = new int[Width * Height]; // each int is the index of a block in the blocks's prefabs array


	static public int xyToIndex(int x, int y)
	{
		return y * Width + x;
	}


	static public bool isPositionEmpty(float x, float y)
	{
		return isPositionEmpty (Mathf.FloorToInt (x), Mathf.FloorToInt (y));
	}


	static public bool isPositionEmpty(int x, int y)
	{
		return (xyToIndex (x, y) == 0);
	}


}

