using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000C9 RID: 201
public class BaldiScript : MonoBehaviour
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x00024564 File Offset: 0x00022964
	private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.timeToMove = this.baseTime;
		this.Wander();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x000245C4 File Offset: 0x000229C4
	private void Update()
	{
		if (this.timeToMove > 0f)
		{
			this.timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			this.Move();
		}
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.baldiTempAnger > 0f)
		{
			this.baldiTempAnger -= 0.02f * Time.deltaTime;
		}
		else
		{
			this.baldiTempAnger = 0f;
		}
		if (this.antiHearingTime > 0f)
		{
			this.antiHearingTime -= Time.deltaTime;
		}
		else
		{
			this.antiHearing = false;
		}
		if (this.endless)
		{
			if (this.timeToAnger > 0f)
			{
				this.timeToAnger -= 1f * Time.deltaTime;
			}
			else
			{
				this.timeToAnger = this.angerFrequency;
				this.GetAngry(this.angerRate);
				this.angerRate += this.angerRateRate;
			}
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x000246F8 File Offset: 0x00022AF8
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f)
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else
		{
			this.agent.speed = 0f;
		}
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			this.db = true;
			this.TargetPlayer();
		}
		else
		{
			this.db = false;
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x000247D0 File Offset: 0x00022BD0
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002480A File Offset: 0x00022C0A
	public void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0002483C File Offset: 0x00022C3C
	private void Move()
	{
		if (base.transform.position == this.previous & this.coolDown < 0f)
		{
			this.Wander();
		}
		this.moveFrames = 10f;
		this.timeToMove = this.baldiWait - this.baldiTempAnger;
		this.previous = base.transform.position;
		this.baldiAudio.PlayOneShot(this.slap);
		this.baldiAnimator.SetTrigger("slap");
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00024930 File Offset: 0x00022D30
	public void GetAngry(float value)
	{
		this.baldiAnger += value;
		if (this.baldiAnger < 0.5f)
		{
			this.baldiAnger = 0.5f;
		}
		this.baldiWait = -3f * this.baldiAnger / (this.baldiAnger + 2f / this.baldiSpeedScale) + 3f;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00024992 File Offset: 0x00022D92
	public void GetTempAngry(float value)
	{
		this.baldiTempAnger += value;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x000249A2 File Offset: 0x00022DA2
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!this.antiHearing && priority >= this.currentPriority)
		{
			this.agent.SetDestination(soundLocation);
			this.currentPriority = priority;
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000249CF File Offset: 0x00022DCF
	public void ActivateAntiHearing(float t)
	{
		this.Wander();
		this.antiHearing = true;
		this.antiHearingTime = t;
	}

	// Token: 0x0400067F RID: 1663
	public bool db;

	// Token: 0x04000680 RID: 1664
	public float baseTime;

	// Token: 0x04000681 RID: 1665
	public float speed;

	// Token: 0x04000682 RID: 1666
	public float timeToMove;

	// Token: 0x04000683 RID: 1667
	public float baldiAnger;

	// Token: 0x04000684 RID: 1668
	public float baldiTempAnger;

	// Token: 0x04000685 RID: 1669
	public float baldiWait;

	// Token: 0x04000686 RID: 1670
	public float baldiSpeedScale;

	// Token: 0x04000687 RID: 1671
	private float moveFrames;

	// Token: 0x04000688 RID: 1672
	private float currentPriority;

	// Token: 0x04000689 RID: 1673
	public bool antiHearing;

	// Token: 0x0400068A RID: 1674
	public float antiHearingTime;

	// Token: 0x0400068B RID: 1675
	public float vibrationDistance;

	// Token: 0x0400068C RID: 1676
	public float angerRate;

	// Token: 0x0400068D RID: 1677
	public float angerRateRate;

	// Token: 0x0400068E RID: 1678
	public float angerFrequency;

	// Token: 0x0400068F RID: 1679
	public float timeToAnger;

	// Token: 0x04000690 RID: 1680
	public bool endless;

	// Token: 0x04000691 RID: 1681
	public Transform player;

	// Token: 0x04000692 RID: 1682
	public Transform wanderTarget;

	// Token: 0x04000693 RID: 1683
	public AILocationSelectorScript wanderer;

	// Token: 0x04000694 RID: 1684
	private AudioSource baldiAudio;

	// Token: 0x04000695 RID: 1685
	public AudioClip slap;

	// Token: 0x04000696 RID: 1686
	public AudioClip[] speech = new AudioClip[3];

	// Token: 0x04000697 RID: 1687
	public Animator baldiAnimator;

	// Token: 0x04000698 RID: 1688
	public float coolDown;

	// Token: 0x04000699 RID: 1689
	private Vector3 previous;

	// Token: 0x0400069B RID: 1691
	private NavMeshAgent agent;
}
