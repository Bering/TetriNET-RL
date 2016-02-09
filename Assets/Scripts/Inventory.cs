using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {


	public int[] InventorySlots;


	void Awake()
	{
		InventorySlots = new int[18];
	}


}
