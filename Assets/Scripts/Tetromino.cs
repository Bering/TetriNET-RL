using UnityEngine;
using System.Collections;

public class Tetromino
{
	public enum Types { O, T, I, S, Z, J, L, Count }

	public Types type;
	public int spriteType;
	public Vector2[] blocks;
	public PlayArea playArea;

	public static Tetromino CreateRandomTetromino()
	{
		int typeInt = Random.Range (0, (int)Types.Count);

		switch(typeInt) {
		case 0: return new Tetromino_O();
		case 1: return new Tetromino_T();
		case 2: return new Tetromino_I();
		case 3: return new Tetromino_S();
		case 4: return new Tetromino_Z();
		case 5: return new Tetromino_J();
		case 6: return new Tetromino_L();
		default:
			throw new UnityException ("Tetromino.SpawnRandomTetromino() : Random.Range returned a number out of range...");
		}
	}

	public void Spawn (PlayArea playArea)
	{
		this.playArea = playArea;
		Vector2 spawnPoint = new Vector2 (PlayArea.spawnX, PlayArea.NumberOfRows - 1);

		for (int i = 0; i < blocks.Length; i++) {
			blocks[i] = spawnPoint - blocks[i];

			playArea.SetBlockType (Mathf.FloorToInt(blocks[i].x), Mathf.FloorToInt(blocks[i].y), spriteType);
		}
	}

}
