using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(PlayArea))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayAreaView : MonoBehaviour
{
	public int blockSizeInPixels = 16;

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

				go = new GameObject ("Block (" + x + "," + y + ")");
				go.transform.SetParent(transform);
				go.transform.position = transform.position +  new Vector3 (x * blockSizeInPixels / 16f, y * blockSizeInPixels / 16f);
				go.transform.rotation = Quaternion.identity;

				grid [convertXYToGridIndex(x,y)] = go.AddComponent<SpriteRenderer> ();

			}
		}

	}


	// Called when GridChangedEvent is invoked
	public void UpdateRenderers()
	{
		for (int y = 0; y < PlayArea.NumberOfRows; y++) {
			for (int x = 0; x < PlayArea.BlocksPerRow; x++) {
				if (blockSizeInPixels == 16) {
					grid [convertXYToGridIndex (x, y)].sprite = Theme.current.blocksSprites16 [playArea.GetBlockType (x, y)];
				}else{
					grid [convertXYToGridIndex (x, y)].sprite = Theme.current.blocksSprites8 [playArea.GetBlockType (x, y)];
				}
			}
		}
	}


	public void OnFullLineFound(int y)
	{
		// TODO: Call SpriteBlaster on each blocks
		playArea.ClearLine (y);
	}


}

