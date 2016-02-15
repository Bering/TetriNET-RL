using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayArea : MonoBehaviour
{
	public const int BlocksPerRow = 12;
	public const int NumberOfRows = 22;
	public const int normalBlocksCount = 5; // Blue, Yellow, Green, Purple, Red
	public const int specialBlocksCount = 9; // A, C, N, R, S, B, G, Q, O
	public const int totalBlocksCount = normalBlocksCount + specialBlocksCount + 1; // +1 for the empty block (i.e. no block)

	public UnityEvent GridChangedEvent;

	protected int[] grid;
	protected bool isDirty;


	void Awake()
	{
		grid = new int[BlocksPerRow * NumberOfRows]; // each int is the index of a block in the blocks's prefabs array
	}


	void Start ()
	{
		// Fill the grid with random crap
		for (int y = 0; y < NumberOfRows; y++) {
			for (int x = 0; x < BlocksPerRow; x++) {
				SetBlockTypeToRandomBlock (x, y);
			}
		}

		StartCoroutine ("GravityEffectForShow");
		StartCoroutine ("SpawnRandomForShow");
	}
	
	
	protected int convertXYToGridIndex(int x, int y)
	{
		Assert.IsTrue (x >= 0 && x < BlocksPerRow);
		Assert.IsTrue (y >= 0 && x < NumberOfRows);
		return (y * BlocksPerRow) + x;
	}
	

	public int GetBlockType(int x, int y)
	{
		Assert.IsNotNull (grid);
		return grid [convertXYToGridIndex (x, y)];
	}


	public void SetBlockType(int x, int y, int spriteIndex)
	{
		grid [convertXYToGridIndex (x, y)] = spriteIndex;
		isDirty = true;
	}


	// for debug
	protected void SetBlockTypeToRandomBlock(int x, int y)
	{
		SetBlockType (x, y, Random.Range (1, totalBlocksCount));// 1 because 0 is empty, we don't want to spawn an empty block
	}

	
	// for debug
	protected IEnumerator  SpawnRandomForShow()
	{
		int x1, x2, x3, x4;

		// Let 1 sec pass for the blocks to fall down one row
		yield return new WaitForSeconds (1);

		while(true)
		{
			x1 = Random.Range (0, BlocksPerRow - 4);
			x2 = Random.Range (x1+1, BlocksPerRow - 3);
			x3 = Random.Range (x2+1, BlocksPerRow - 2);
			x4 = Random.Range (x3+1, BlocksPerRow - 1);
			
			SetBlockTypeToRandomBlock (x1, NumberOfRows - 1);
			SetBlockTypeToRandomBlock (x2, NumberOfRows - 1);
			SetBlockTypeToRandomBlock (x3, NumberOfRows - 1);
			SetBlockTypeToRandomBlock (x4, NumberOfRows - 1);

			isDirty = true;

			yield return new WaitForSeconds (1);
		}
	}
	

	// for debug
	protected IEnumerator  GravityEffectForShow()
	{
		int x, y;

		while(true)
		{

			for (y = 0; y < NumberOfRows - 1; y++) { // -1 because we grab the blocks above y
				for (x = 0; x < BlocksPerRow; x++) {
					grid [convertXYToGridIndex (x, y)] = grid [convertXYToGridIndex (x, y + 1)];
				}
			}

			for (x = 0; x < PlayArea.BlocksPerRow; x++) {
				SetBlockType(x, NumberOfRows-1, 0);
			}

			isDirty = true;

			yield return new WaitForSeconds (1);
		}
	}


	void Update()
	{
		if (isDirty) {
			GridChangedEvent.Invoke ();
			isDirty = false;
		}
	}


}

