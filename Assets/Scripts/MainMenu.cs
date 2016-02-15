using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	void Awake()
	{
		DontDestroyOnLoad(transform.parent.gameObject);
	}


	public void OnExitButtonPushed()
	{
		#if UNITY_STANDALONE
		Application.Quit();
		#endif

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}


	public void OnSoloButtonPushed()
	{
		SceneManager.LoadScene (1);
	}


}
