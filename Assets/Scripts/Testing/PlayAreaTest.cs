using UnityEngine;
using System.Collections;


public class PlayAreaTest : MonoBehaviour {


	protected PlayArea[] playAreas;


	void Awake()
	{
		playAreas = GameObject.FindObjectsOfType<PlayArea> ();
	}
	
	
	void Start()
	{
		for (int i = 0; i < playAreas.Length; i++) {
			for (int y = 0; y < PlayArea.NumberOfRows; y++) {
				for (int x = 0; x < PlayArea.BlocksPerRow; x++) {
					SetBlockTypeToRandomBlock (playAreas [i], x, y);
				}
			}
		}
		
		StartCoroutine ("Gravity");
		StartCoroutine ("SpawnRandomBlocks");
	}


	protected void SetBlockTypeToRandomBlock(PlayArea playArea, int x, int y)
	{
		playArea.SetBlockType (x, y, Random.Range (1, PlayArea.totalBlocksCount));// 1 because 0 is empty, we don't want to spawn an empty block
	}


	protected IEnumerator Gravity()
	{
		int x, y;

		// Let 1 sec pass so we can make sure that the playArea is full
		yield return new WaitForSeconds (1);

		while(true)
		{

			for (int i = 0; i < playAreas.Length; i++) {
				
				for (y = 0; y < PlayArea.NumberOfRows - 1; y++) { // -1 because we grab the blocks above y
					for (x = 0; x < PlayArea.BlocksPerRow; x++) {
						playAreas[i].SetBlockType(x, y, playAreas[i].GetBlockType (x, y + 1));
					}
				}

				for (x = 0; x < PlayArea.BlocksPerRow; x++) {
					playAreas[i].SetBlockType(x, PlayArea.NumberOfRows-1, 0);
				}
			}

			yield return new WaitForSeconds (1);
		}
	}


	protected IEnumerator SpawnRandomBlocks()
	{
		int x1, x2, x3, x4;
		
		// Let 1.5 sec pass for the blocks to fall down one row
		yield return new WaitForSeconds (1.5f);

		while(true)
		{
			for (int i = 0; i < playAreas.Length; i++) {
				
				x1 = Random.Range (0, PlayArea.BlocksPerRow);
				x2 = Random.Range (0, PlayArea.BlocksPerRow);
				x3 = Random.Range (0, PlayArea.BlocksPerRow);
				x4 = Random.Range (0, PlayArea.BlocksPerRow);
				
				while(x2 == x1 || x2 == x3 || x2 == x4) {
					x2 = Random.Range (0, PlayArea.BlocksPerRow);
				}
				
				while(x3 == x1 || x3 == x2 || x3 == x4) {
					x3 = Random.Range (0, PlayArea.BlocksPerRow);
				}
				
				while(x4 == x1 || x4 == x2 || x4 == x3) {
					x4 = Random.Range (0, PlayArea.BlocksPerRow);
				}
				
				SetBlockTypeToRandomBlock (playAreas[i], x1, PlayArea.NumberOfRows - 1);
				SetBlockTypeToRandomBlock (playAreas[i], x2, PlayArea.NumberOfRows - 1);
				SetBlockTypeToRandomBlock (playAreas[i], x3, PlayArea.NumberOfRows - 1);
				SetBlockTypeToRandomBlock (playAreas[i], x4, PlayArea.NumberOfRows - 1);
				
			}
			
			yield return new WaitForSeconds (1);
		}
	}
	
	
}

