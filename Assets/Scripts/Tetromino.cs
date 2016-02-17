using UnityEngine;
using System.Collections;

public class Tetromino
{
	public enum Types { O, T, I, S, Z, J, L, Count }

	public Types type;
	public int spriteType;
	public Vector2[] blocks;


	public static Tetromino SpawnRandomTetromino(PlayArea playArea)
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

	public virtual void Spawn (PlayArea playArea)
	{
		Vector2 spawnPoint = new Vector2 (PlayArea.spawnX, PlayArea.NumberOfRows - 1);

		for (int i = 0; i < blocks.Length; i++) {
			blocks[i] += spawnPoint;

			playArea.SetBlockType (Mathf.FloorToInt(blocks[i].x), Mathf.FloorToInt(blocks[i].y), spriteType);
		}
	}

}

public class Tetromino_O : Tetromino
{
	public Tetromino_O()
	{
		spriteType = 2;
		type = Tetromino.Types.O;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(0,-1),
			new Vector2(1,-1)
		};
	}
}

public class Tetromino_T : Tetromino
{
	public Tetromino_T()
	{
		spriteType = 2;
		type = Tetromino.Types.T;
		blocks = new Vector2[] {
			new Vector2(-1,0),
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(0,-1)
		};
	}
}

public class Tetromino_I : Tetromino
{
	public Tetromino_I()
	{
		spriteType = 1;
		type = Tetromino.Types.I;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,-1),
			new Vector2(0,-2),
			new Vector2(0,-3)
		};
	}
}

public class Tetromino_S : Tetromino
{
	public Tetromino_S()
	{
		spriteType = 1;
		type = Tetromino.Types.S;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(-1,-1),
			new Vector2(0,-1)
		};
	}
}

public class Tetromino_Z : Tetromino
{
	public Tetromino_Z()
	{
		spriteType = 5;
		type = Tetromino.Types.Z;
		blocks = new Vector2[] {
			new Vector2(-1,0),
			new Vector2(0,0),
			new Vector2(0,-1),
			new Vector2(1,-1)
		};
	}
}

public class Tetromino_J : Tetromino
{
	public Tetromino_J()
	{
		spriteType = 3;
		type = Tetromino.Types.J;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,-1),
			new Vector2(-1,-2),
			new Vector2(0,-2)
		};
	}
}

public class Tetromino_L : Tetromino
{
	public Tetromino_L()
	{
		spriteType = 4;
		type = Tetromino.Types.L;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,-1),
			new Vector2(0,-2),
			new Vector2(1,-2)
		};
	}
}
