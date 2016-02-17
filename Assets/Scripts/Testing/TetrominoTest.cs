using UnityEngine;
using System.Collections;

public class TetrominoTest : MonoBehaviour {


	PlayArea[] playAreas;


	void Awake()
	{
		playAreas = FindObjectsOfType<PlayArea> ();
	}


	void Start()
	{
		StartCoroutine ("SpawnRandom");
		StartCoroutine ("Gravity");
	}


	public IEnumerator SpawnRandom()
	{
		Tetromino tetromino;

		while (true) {
			
			for (int i = 0; i < playAreas.Length; i++) {
				tetromino = Tetromino.SpawnRandomTetromino (playAreas [i]);
				tetromino.Spawn (playAreas [i]);
			}

			yield return new WaitForSeconds (4);
		}

	}


	protected IEnumerator Gravity()
	{
		int x, y;

		yield return new WaitForSeconds (0.5f);

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

}
