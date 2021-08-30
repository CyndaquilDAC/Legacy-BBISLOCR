using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AlarmClockScript : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000458
	private void Start()
	{
		this.timeLeft = 30f;
		this.lifeSpan = 35f;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000470
	private void Update()
	{
		if (this.timeLeft >= 0f)
		{
			this.timeLeft -= Time.deltaTime;
		}
		else if (!this.rang)
		{
			this.Alarm();
		}
		if (this.lifeSpan >= 0f)
		{
			this.lifeSpan -= Time.deltaTime;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject, 0f);
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000004EC
	private void Alarm()
	{
		this.rang = true;
		this.baldi.Hear(base.transform.position, 8f);
		this.audioDevice.clip = this.ring;
		this.audioDevice.loop = false;
		this.audioDevice.Play();
	}

	// Token: 0x04000001 RID: 1
	public float timeLeft;

	// Token: 0x04000002 RID: 2
	private float lifeSpan;

	// Token: 0x04000003 RID: 3
	private bool rang;

	// Token: 0x04000004 RID: 4
	public BaldiScript baldi;

	// Token: 0x04000005 RID: 5
	public AudioClip ring;

	// Token: 0x04000006 RID: 6
	public AudioSource audioDevice;
}
