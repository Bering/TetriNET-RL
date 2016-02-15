using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayArea))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayAreaView : MonoBehaviour
{
	public int blockSizeInPixels = 16;
	public Sprite playAreaBackground;
	public List<Sprite> blocksSprites;
	public GameObject blockPrefab;

	protected PlayArea playArea;


	protected void Awake()
	{
		playArea = GetComponent<PlayArea> ();
	}


	void Start()
	{
		for (int y = 0; y < PlayArea.Height; y++) {
			for (int x = 0; x < PlayArea.Width; x++) {
				SpawnBlock (x, y);
			}
		}

		StartCoroutine ("SpawnRandomForShow");
	}


	void SpawnBlock(float x, float y)
	{
		GameObject go = null;
		Vector3 pos = transform.position;

		if (blockSizeInPixels == 16) {
			go = Instantiate(blockPrefab, new Vector3 (pos.x + x, pos.y + y), Quaternion.identity) as GameObject;
		} else {
			go = Instantiate(blockPrefab, new Vector3 (pos.x + x/2f, pos.y + y/2f), Quaternion.identity) as GameObject;
		}

		go.name = "Block (" + x + "," + y + ")";
		go.transform.parent = transform;

		//go.GetComponent<SpriteRenderer>().sprite = blocksSprites[playArea.GetSpriteIndex (x,y)];
		go.GetComponent<SpriteRenderer>().sprite = blocksSprites[Random.Range(1, blocksSprites.Count)];
	}


	protected IEnumerator  SpawnRandomForShow()
	{
		int x1, x2, x3, x4,  right;
		Vector3 pos = transform.position;

		right = 11;

		while(true)
		{
			x1 = Random.Range (0, right - 3);
			x2 = Random.Range (x1+1, right - 2);
			x3 = Random.Range (x2+1, right - 1);
			x4 = Random.Range (x3+1, right);

			SpawnBlock (x1, 21);
			SpawnBlock (x2, 21);
			SpawnBlock (x3, 21);
			SpawnBlock (x4, 21);

			yield return new WaitForSeconds (1);
		}
	}


}

