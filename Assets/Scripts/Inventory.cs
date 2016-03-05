using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {


	public int[] inventorySlots;

	public UnityEvent InventoryChangedEvent;

	protected bool isDirty;


	void Awake()
	{
		inventorySlots = new int[18];
	}


	void Update()
	{
		if (isDirty) {
			InventoryChangedEvent.Invoke ();
			isDirty = false;
		}
	}


}
