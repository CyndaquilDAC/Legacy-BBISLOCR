using System;
using TMPro;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class JumpRopeScript : MonoBehaviour
{
	// Token: 0x06000048 RID: 72 RVA: 0x000033A4 File Offset: 0x000017A4
	private void OnEnable()
	{
		this.jumpDelay = 1f;
		this.ropeHit = true;
		this.jumpStarted = false;
		this.jumps = 0;
		this.jumpCount.text = 0 + "/5";
		this.cs.jumpHeight = 0f;
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_ReadyGo);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003418 File Offset: 0x00001818
	private void Update()
	{
		if (this.jumpDelay > 0f)
		{
			this.jumpDelay -= Time.deltaTime;
		}
		else if (!this.jumpStarted)
		{
			this.jumpStarted = true;
			this.ropePosition = 1f;
			this.rope.SetTrigger("ActivateJumpRope");
			this.ropeHit = false;
		}
		if (this.ropePosition > 0f)
		{
			this.ropePosition -= Time.deltaTime;
		}
		else if (!this.ropeHit)
		{
			this.RopeHit();
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000034B8 File Offset: 0x000018B8
	private void RopeHit()
	{
		this.ropeHit = true;
		if (this.cs.jumpHeight <= 0.2f)
		{
			this.Fail();
		}
		else
		{
			this.Success();
		}
		this.jumpStarted = false;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000034F0 File Offset: 0x000018F0
	private void Success()
	{
		this.playtime.audioDevice.Stop();
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Numbers[this.jumps]);
		this.jumps++;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 0.5f;
		if (this.jumps >= 5)
		{
			this.playtime.audioDevice.Stop();
			this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Congrats);
			this.ps.DeactivateJumpRope();
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000035AC File Offset: 0x000019AC
	private void Fail()
	{
		this.jumps = 0;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 2f;
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Oops);
	}

	// Token: 0x04000058 RID: 88
	public TMP_Text jumpCount;

	// Token: 0x04000059 RID: 89
	public Animator rope;

	// Token: 0x0400005A RID: 90
	public CameraScript cs;

	// Token: 0x0400005B RID: 91
	public PlayerScript ps;

	// Token: 0x0400005C RID: 92
	public PlaytimeScript playtime;

	// Token: 0x0400005E RID: 94
	public int jumps;

	// Token: 0x0400005F RID: 95
	public float jumpDelay;

	// Token: 0x04000060 RID: 96
	public float ropePosition;

	// Token: 0x04000061 RID: 97
	public bool ropeHit;

	// Token: 0x04000062 RID: 98
	public bool jumpStarted;
	
	public TMP_Text instructions;
}
