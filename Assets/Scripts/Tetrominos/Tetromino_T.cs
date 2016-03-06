using UnityEngine;

public class Tetromino_T : Tetromino
{
	public Tetromino_T()
	{
		spriteType = 2;
		type = Tetromino.Types.T;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(-1,0),
			new Vector2(1,0),
			new Vector2(0,1)
		};
	}
}

