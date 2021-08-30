using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class DoorScript : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x0002114F File Offset: 0x0001F54F
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00021170 File Offset: 0x0001F570
	private void Update()
	{
		if (this.lockTime > 0f)
		{
			this.lockTime -= 1f * Time.deltaTime;
		}
		else if (this.bDoorLocked)
		{
			this.UnlockDoor();
		}
		if (this.openTime > 0f)
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.openTime <= 0f & this.bDoorOpen)
		{
			this.barrier.enabled = true;
			this.invisibleBarrier.enabled = true;
			this.bDoorOpen = false;
			this.inside.material = this.closed;
			this.outside.material = this.closed;
			if (this.silentOpens <= 0)
			{
				this.myAudio.PlayOneShot(this.doorClose, 1f);
			}
		}
		if (Input.GetButtonDown("Interact") & Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == this.trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance & !this.bDoorLocked))
			{
				if (this.baldi.isActiveAndEnabled & this.silentOpens <= 0)
				{
					this.baldi.Hear(base.transform.position, 1f);
				}
				this.OpenDoor();
				if (this.silentOpens > 0)
				{
					this.silentOpens--;
				}
			}
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0002135C File Offset: 0x0001F75C
	public void OpenDoor()
	{
		if (this.silentOpens <= 0 && !this.bDoorOpen)
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
		}
		this.barrier.enabled = false;
		this.invisibleBarrier.enabled = false;
		this.bDoorOpen = true;
		this.inside.material = this.open;
		this.outside.material = this.open;
		this.openTime = 3f;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x000213E2 File Offset: 0x0001F7E2
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC"))
		{
			this.OpenDoor();
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00021404 File Offset: 0x0001F804
	public void LockDoor(float time)
	{
		this.bDoorLocked = true;
		this.lockTime = time;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00021414 File Offset: 0x0001F814
	public void UnlockDoor()
	{
		this.bDoorLocked = false;
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06000940 RID: 2368 RVA: 0x0002141D File Offset: 0x0001F81D
	public bool DoorLocked
	{
		get
		{
			return this.bDoorLocked;
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00021425 File Offset: 0x0001F825
	public void SilenceDoor()
	{
		this.silentOpens = 4;
	}

	// Token: 0x040005C3 RID: 1475
	public float openingDistance;

	// Token: 0x040005C4 RID: 1476
	public Transform player;

	// Token: 0x040005C5 RID: 1477
	public BaldiScript baldi;

	// Token: 0x040005C6 RID: 1478
	public MeshCollider barrier;

	// Token: 0x040005C7 RID: 1479
	public MeshCollider trigger;

	// Token: 0x040005C8 RID: 1480
	public MeshCollider invisibleBarrier;

	// Token: 0x040005C9 RID: 1481
	public MeshRenderer inside;

	// Token: 0x040005CA RID: 1482
	public MeshRenderer outside;

	// Token: 0x040005CB RID: 1483
	public AudioClip doorOpen;

	// Token: 0x040005CC RID: 1484
	public AudioClip doorClose;

	// Token: 0x040005CD RID: 1485
	public Material closed;

	// Token: 0x040005CE RID: 1486
	public Material open;

	// Token: 0x040005CF RID: 1487
	public bool bDoorOpen;

	// Token: 0x040005D0 RID: 1488
	public bool bDoorLocked;

	// Token: 0x040005D1 RID: 1489
	public int silentOpens;

	// Token: 0x040005D2 RID: 1490
	private float openTime;

	// Token: 0x040005D3 RID: 1491
	public float lockTime;

	// Token: 0x040005D4 RID: 1492
	private AudioSource myAudio;
}
