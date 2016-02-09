using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Inventory))]
public class InventoryView : MonoBehaviour {


	protected Inventory inventory;
	protected SpriteRenderer[] inventorySlotViews;


	void Awake()
	{
		inventory = GetComponent<Inventory> ();
		inventorySlotViews = new SpriteRenderer[18];
	}


	void Start () {
		
		for (int i = 0; i < inventory.InventorySlots.Length; i++) {
			// TODO: OMG what am I doing!!? Time to go to bed LOL
			inventorySlotViews[i] = transform.FindChild("Inventory_"+i).gameObject.GetComponent<SpriteRenderer> ();

			inventory.InventorySlots[i]= Random.Range (6, 15);
			inventorySlotViews [i].sprite = GameObject.Find ("PlayArea").GetComponent<PlayAreaView> ().blocksSprites[inventory.InventorySlots [i]];
		}

	}

}
