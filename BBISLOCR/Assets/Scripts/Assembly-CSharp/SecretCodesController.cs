using System;
using TMPro;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class SecretCodesController : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002399 File Offset: 0x00000599
	public void GOOSHOES()
	{
		this.text.text = "<color=yellow>W<color=blue>O<color=green>W! <color=black>THAT`S GOOSHOES, BABY!";
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000023BB File Offset: 0x000005BB
	public void PLACEFACE()
	{
		this.text.text = "THIS IS WHERE IT ALL <color=red>BEGAN";
		PlayerPrefs.SetString("HasSeenTestroom", "WhereItAllBegan");
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TestRoom");
	}


	// Token: 0x04000013 RID: 19
	public TMP_Text text;
}
