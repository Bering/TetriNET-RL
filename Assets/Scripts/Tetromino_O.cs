using UnityEngine;

public class Tetromino_O : Tetromino
{
	public Tetromino_O()
	{
		spriteType = 2;
		type = Tetromino.Types.O;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(0,1),
			new Vector2(1,1)
		};
	}
}
