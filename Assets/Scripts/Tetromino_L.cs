using UnityEngine;

public class Tetromino_L : Tetromino
{
	public Tetromino_L()
	{
		spriteType = 4;
		type = Tetromino.Types.L;
		blocks = new Vector2[] {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(0,2),
			new Vector2(1,2)
		};
	}
}
