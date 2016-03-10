using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System;

public class ActivityLog : MonoBehaviour {

	protected List<string> lines;

	[Serializable] public class ContentChangedEventClass : UnityEvent<string> {}
	public ContentChangedEventClass ContentChangedEvent;


	void Awake () {
		Services.Register<ActivityLog> (this);

		lines = new List<string>();
		// TODO: Register as a service
	}
	

	public void Append(string playerName, string activity)
	{
		lines.Add("<i>"+playerName+"</i> > " + activity);

		int index = Mathf.Max (lines.Count - 10, 0);
		int count = Mathf.Min (lines.Count, 10);

		ContentChangedEvent.Invoke (String.Join("\n", lines.GetRange(index, count).ToArray ()));
	}


}
