using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(PlayArea))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayAreaView : MonoBehaviour
{
	public int blockSizeInPixels = 16;
	public List<Sprite> blocksSprites;
	public Sprite playAreaBackground;

	protected PlayArea playArea;
	protected SpriteRenderer[] grid;


	void Awake()
	{
		playArea = GetComponent<PlayArea> ();
	}


	void Start()
	{
		SpawnRenderers();
	}


	protected int convertXYToGridIndex(int x, int y)
	{
		Assert.IsTrue (x >= 0 && x < PlayArea.BlocksPerRow);
		Assert.IsTrue (y >= 0 && x < PlayArea.NumberOfRows);
		return (y * PlayArea.BlocksPerRow) + x;
	}


	protected void SpawnRenderers()
	{
		grid = new SpriteRenderer[PlayArea.BlocksPerRow * PlayArea.NumberOfRows];

		GameObject go = null;

		for (int y = 0; y < PlayArea.NumberOfRows; y++) {
			for (int x = 0; x < PlayArea.BlocksPerRow; x++) {

				go = Instantiate(new GameObject(), new Vector3 (x * blockSizeInPixels/16f, y * blockSizeInPixels/16f), Quaternion.identity) as GameObject; // Why 16? Because original theme in BMP file is 16x16
				go.name = "Block (" + x + "," + y + ")";
				go.transform.SetParent(transform, false);

				grid [convertXYToGridIndex(x,y)] = go.AddComponent<SpriteRenderer> ();

			}
		}
	}


	// Called when GridChangedEvent is invoked
	public void UpdateRenderers()
	{
		for (int y = 0; y < PlayArea.NumberOfRows; y++) {
			for (int x = 0; x < PlayArea.BlocksPerRow; x++) {
				grid [convertXYToGridIndex (x, y)].sprite = blocksSprites[playArea.GetBlockType (x,y)];
			}
		}
	}


}

