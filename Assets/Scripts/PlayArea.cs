using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class PlayArea : MonoBehaviour
{
	public const int BlocksPerRow = 12;
	public const int NumberOfRows = 22;
	public const int normalBlocksCount = 5; // Blue, Yellow, Green, Purple, Red
	public const int specialBlocksCount = 9; // A, C, N, R, S, B, G, Q, O
	public const int totalBlocksCount = normalBlocksCount + specialBlocksCount + 1; // +1 for the empty block (i.e. no block)
	public const int spawnX = 5;

	public UnityEvent GridChangedEvent;

	protected int[] grid;
	protected bool isDirty;


	void Awake()
	{
		Clear ();
	}

	
	public void Clear()
	{
		grid = new int[BlocksPerRow * NumberOfRows]; // each int is the index of a block in the blocks's prefabs array
	}

	
	protected int convertXYToGridIndex(int x, int y)
	{
		Assert.IsTrue (x >= 0 && x < BlocksPerRow);
		Assert.IsTrue (y >= 0 && y < NumberOfRows);
		return (y * BlocksPerRow) + x;
	}
	

	public int GetBlockType(int x, int y)
	{
		Assert.IsNotNull (grid);
		return grid [convertXYToGridIndex (x, y)];
	}
	public int GetBlockType(Vector2 v)
	{
		return GetBlockType (Mathf.FloorToInt (v.x), Mathf.FloorToInt (v.y));
	}


	public void SetBlockType(int x, int y, int spriteIndex)
	{
		grid [convertXYToGridIndex (x, y)] = spriteIndex;
		isDirty = true;
	}
	public void SetBlockType(Vector2 v, int spriteIndex)
	{
		SetBlockType (Mathf.FloorToInt (v.x), Mathf.FloorToInt (v.y), spriteIndex);
	}


	void Update()
	{
		if (isDirty) {
			GridChangedEvent.Invoke ();
			isDirty = false;
		}
	}


	public int ClearFullLines()
	{
		int linesCleared = 0;

		for (int y = 0; y < NumberOfRows; y++) {
			if (isLineFull (y)) {
				ClearLine (y);
				linesCleared++;
				y--; // redo this line, now that it contains the stuff that was above
			}
		}

		return linesCleared;
	}


	protected bool isLineFull(int y)
	{
		for (int x = 0; x < BlocksPerRow; x++) {
			if (GetBlockType (x,y) == 0) {
				return false;
			}
		}

		return true;
	}


	protected void ClearLine(int clearY)
	{
		int x, y;

		for (y = clearY; y < NumberOfRows-1; y++) {
			for (x = 0; x < BlocksPerRow; x++) {
				SetBlockType (x, y, GetBlockType (x, y + 1));
			}
		}
		
		for (x = 0; x < BlocksPerRow; x++) {
			SetBlockType (x, NumberOfRows-1, 0);
		}
		
	}

}

