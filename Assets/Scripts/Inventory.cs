using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {


	public int[] inventorySlots;

	public UnityEvent InventoryChangedEvent;


	void Awake()
	{
		inventorySlots = new int[18];
	}


	void Start()
	{
		for (int i = 0; i < inventorySlots.Length; i++) {
			inventorySlots[i]= Random.Range (PlayArea.normalBlocksCount+1, PlayArea.totalBlocksCount); // +1 because of the empty block
		}

		InventoryChangedEvent.Invoke ();
	}


}
