using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D4 RID: 212
public class WarningScreenScript : MonoBehaviour
{
	private void Start()
	{
	}
	
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
