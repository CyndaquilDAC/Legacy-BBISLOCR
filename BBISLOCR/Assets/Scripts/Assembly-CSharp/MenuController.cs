using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class MenuController : MonoBehaviour
{
	// Token: 0x0600004E RID: 78 RVA: 0x0000360E File Offset: 0x00001A0E
	private void Start()
	{
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003621 File Offset: 0x00001A21
	public void OnEnable()
	{
		this.uc.firstButton = this.firstButton;
		this.uc.dummyButtonPC = this.dummyButtonPC;
		this.uc.dummyButtonElse = this.dummyButtonElse;
		this.uc.SwitchMenu();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003661 File Offset: 0x00001A61
	private void Update()
	{
		if (Input.GetButtonDown("Cancel") && this.back != null)
		{
			this.back.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000063 RID: 99
	public UIController uc;

	// Token: 0x04000064 RID: 100
	public Selectable firstButton;

	// Token: 0x04000065 RID: 101
	public Selectable dummyButtonPC;

	// Token: 0x04000066 RID: 102
	public Selectable dummyButtonElse;

	// Token: 0x04000067 RID: 103
	public GameObject back;
}
