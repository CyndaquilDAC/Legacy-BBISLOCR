using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000CA RID: 202
public class CraftersScript : MonoBehaviour
{
	// Token: 0x060009AE RID: 2478 RVA: 0x000249ED File Offset: 0x00022DED
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
		this.sprite.SetActive(false);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00024A08 File Offset: 0x00022E08
	private void Update()
	{
		if (this.forceShowTime > 0f)
		{
			this.forceShowTime -= Time.deltaTime;
		}
		if (this.gettingAngry)
		{
			this.anger += Time.deltaTime;
			if (this.anger >= 1f & !this.angry)
			{
				this.angry = true;
				this.audioDevice.PlayOneShot(this.aud_Intro);
				this.spriteImage.sprite = this.angrySprite;
			}
		}
		else if (this.anger > 0f)
		{
			this.anger -= Time.deltaTime;
		}
		if (!this.angry)
		{
			if (((base.transform.position - this.agent.destination).magnitude <= 20f & (base.transform.position - this.player.position).magnitude >= 60f) || this.forceShowTime > 0f)
			{
				this.sprite.SetActive(true);
			}
			else
			{
				this.sprite.SetActive(false);
			}
		}
		else
		{
			this.agent.speed = this.agent.speed + 60f * Time.deltaTime;
			this.TargetPlayer();
			if (!this.audioDevice.isPlaying)
			{
				this.audioDevice.PlayOneShot(this.aud_Loop);
			}
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00024BAC File Offset: 0x00022FAC
	private void FixedUpdate()
	{
		if (this.gc.notebooks >= 7)
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & this.craftersRenderer.isVisible & this.sprite.activeSelf)
			{
				this.gettingAngry = true;
			}
			else
			{
				this.gettingAngry = false;
			}
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00024C65 File Offset: 0x00023065
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!this.angry && this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(location);
			if (flee)
			{
				this.forceShowTime = 3f;
			}
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00024CA0 File Offset: 0x000230A0
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00024CBC File Offset: 0x000230BC
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & this.angry)
		{
			this.player.position = new Vector3(5f, this.player.position.y, 80f);
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f));
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z));
			this.gc.DespawnCrafters();
		}
	}

	// Token: 0x0400069D RID: 1693
	public bool db;

	// Token: 0x0400069E RID: 1694
	public bool angry;

	// Token: 0x0400069F RID: 1695
	public bool gettingAngry;

	// Token: 0x040006A0 RID: 1696
	public float anger;

	// Token: 0x040006A1 RID: 1697
	private float forceShowTime;

	// Token: 0x040006A2 RID: 1698
	public Transform player;

	// Token: 0x040006A3 RID: 1699
	public Transform playerCamera;

	// Token: 0x040006A4 RID: 1700
	public Transform baldi;

	// Token: 0x040006A5 RID: 1701
	public NavMeshAgent baldiAgent;

	// Token: 0x040006A6 RID: 1702
	public GameObject sprite;

	// Token: 0x040006A7 RID: 1703
	public GameControllerScript gc;

	// Token: 0x040006A8 RID: 1704
	[SerializeField]
	private NavMeshAgent agent;

	// Token: 0x040006A9 RID: 1705
	public Renderer craftersRenderer;

	// Token: 0x040006AA RID: 1706
	public SpriteRenderer spriteImage;

	// Token: 0x040006AB RID: 1707
	public Sprite angrySprite;

	// Token: 0x040006AC RID: 1708
	private AudioSource audioDevice;

	// Token: 0x040006AD RID: 1709
	public AudioClip aud_Intro;

	// Token: 0x040006AE RID: 1710
	public AudioClip aud_Loop;
}
