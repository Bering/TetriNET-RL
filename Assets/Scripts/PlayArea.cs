using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour
{
	public const int Width = 12;
	public const int Height = 22;

	protected int[] grid;


	void Awake()
	{
		grid = new int[Width * Height]; // each int is the index of a block in the blocks's prefabs array
	}


	public int GetSpriteIndex(int x, int y)
	{
		return grid [xyToGridIndex (x, y)];
	}


	protected int xyToGridIndex(int x, int y)
	{
		return (y * Width) + x;
	}


	public bool isPositionEmpty(float x, float y)
	{
		return isPositionEmpty (Mathf.FloorToInt (x), Mathf.FloorToInt (y));
	}


	public bool isPositionEmpty(int x, int y)
	{
		return (xyToGridIndex (x, y) == 0);
	}


}

