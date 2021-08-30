using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000C0 RID: 192
public class GameControllerScript : MonoBehaviour
{
	public GameObject Trevor;
	// Token: 0x06000963 RID: 2403 RVA: 0x00021A00 File Offset: 0x0001FE00
	public GameControllerScript()
	{
		int[] array = new int[3];
		array[0] = -80;
		array[1] = -40;
		this.itemSelectOffset = array;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00021AC4 File Offset: 0x0001FEC4
	private void Start()
	{
		this.cullingMask = this.camera.cullingMask;
		this.audioDevice = base.GetComponent<AudioSource>();
		this.mode = PlayerPrefs.GetString("CurrentMode");
		if (this.mode == "endless")
		{
			this.baldiScrpt.endless = true;
		}
		this.schoolMusic.Play();
		this.LockMouse();
		this.UpdateNotebookCount();
		this.itemSelected = 0;
		this.gameOverDelay = 0.5f;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00021B5C File Offset: 0x0001FF5C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			UseItem();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			LookDown();
		}
		if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame();
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Y) & this.gamePaused)
			{
				this.ExitGame();
			}
			else if (Input.GetKeyDown(KeyCode.N) & this.gamePaused)
			{
				this.UnpauseGame();
			}
			if (!this.gamePaused & Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}
			if (Input.GetButtonDown("Item") & Time.timeScale != 0f)
			{
				this.UseItem();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f & Time.timeScale != 0f)
			{
				this.DecreaseItemSelection();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f & Time.timeScale != 0f)
			{
				this.IncreaseItemSelection();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1) & Time.timeScale != 0f)
			{
				this.itemSelected = 0;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) & Time.timeScale != 0f)
			{
				this.itemSelected = 1;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3) & Time.timeScale != 0f)
			{
				this.itemSelected = 2;
				this.UpdateItemSelection();
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
		if (this.player.stamina < 0f & !this.warning.activeSelf)
		{
			this.warning.SetActive(true);
		}
		else if (this.player.stamina > 0f & this.warning.activeSelf)
		{
			this.warning.SetActive(false);
		}
		if (this.player.gameOver)
		{
			if (this.mode == "endless" && this.notebooks > PlayerPrefs.GetInt("HighBooks") && !this.highScoreText.activeSelf)
			{
				this.highScoreText.SetActive(true);
			}
			Time.timeScale = 0f;
			this.gameOverDelay -= Time.unscaledDeltaTime * 0.5f;
			this.camera.farClipPlane = this.gameOverDelay * 400f;
			this.audioDevice.PlayOneShot(this.aud_buzz);
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "endless")
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks"))
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks);
					}
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks);
				}
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00021F8C File Offset: 0x0002038C
	public void UpdateNotebookCount()
	{
		if (this.mode != "endless")
		{
			this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		}
		else
		{
			this.notebookCount.text = this.notebooks.ToString() + " Notebooks";
		}
		if (this.notebooks == 7 & this.mode != "endless")
		{
			this.ActivateFinaleMode();
		}
	}

	public void LookDown()
	{
		if (camLookDown.activeSelf == false)
		{
			camLookDown.SetActive(true);
		}
		else if (camLookDown.activeSelf == true)
		{
			camLookDown.SetActive(false);
		}
	}

	public GameObject camLookDown;

	// Token: 0x06000967 RID: 2407 RVA: 0x00022024 File Offset: 0x00020424
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
		DSCI0000.color = dsciLevels[notebooks];
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0002203A File Offset: 0x0002043A
	public void LockMouse()
	{
		if (!this.learningActive)
		{
			this.cursorController.LockCursor();
			this.mouseLocked = true;
			this.reticle.SetActive(true);
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00022065 File Offset: 0x00020465
	public void UnlockMouse()
	{
		this.cursorController.UnlockCursor();
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00022085 File Offset: 0x00020485
	public void PauseGame()
	{
		if (!this.learningActive)
		{
			this.UnlockMouse();
			Time.timeScale = 0f;
			this.gamePaused = true;
			this.pauseMenu.SetActive(true);
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x000220C5 File Offset: 0x000204C5
	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x000220D1 File Offset: 0x000204D1
	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseMenu.SetActive(false);
		this.LockMouse();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000220F8 File Offset: 0x000204F8
	public void ActivateSpoopMode()
	{
		this.spoopMode = true;
		this.entrance_0.Lower();
		this.entrance_1.Lower();
		this.entrance_2.Lower();
		this.entrance_3.Lower();
		this.baldiTutor.SetActive(false);
		if(mode != "Freerun")
		{
			this.baldi.SetActive(true);
		}
		this.principal.SetActive(true);
		this.crafters.SetActive(true);
		this.playtime.SetActive(true);
		this.gottaSweep.SetActive(true);
		this.Trevor.SetActive(true);
		this.bully.SetActive(true);
		this.firstPrize.SetActive(true);
		this.badSum.SetActive(true);
		this.audioDevice.PlayOneShot(this.aud_Hang);
		this.learnMusic.Stop();
		this.schoolMusic.Stop();
		this.audioDevice.PlayOneShot(this.aud_ReadyOrNot);
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x000221BF File Offset: 0x000205BF
	public void ActivateFinaleMode()
	{
		this.finaleMode = true;
		this.entrance_0.Raise();
		this.entrance_1.Raise();
		this.entrance_2.Raise();
		this.entrance_3.Raise();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000221F4 File Offset: 0x000205F4
	public void GetAngry(float value)
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00022214 File Offset: 0x00020614
	public void ActivateLearningGame()
	{
		this.camera.cullingMask = 0;
		this.learningActive = true;
		this.UnlockMouse();
		this.tutorBaldi.Stop();
		if (!this.spoopMode)
		{
			this.schoolMusic.Stop();
			this.learnMusic.Play();
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00022278 File Offset: 0x00020678
	public void DeactivateLearningGame(GameObject subject)
	{
		this.camera.cullingMask = this.cullingMask;
		this.learningActive = false;
		UnityEngine.Object.Destroy(subject);
		this.LockMouse();
		if (this.player.stamina < 100f)
		{
			this.player.stamina = 100f;
		}
		if (!this.spoopMode)
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}
		if (this.notebooks == 1 & !this.spoopMode)
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == 7 & this.mode != "endless")
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
		}
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00022360 File Offset: 0x00020760
	private void IncreaseItemSelection()
	{
		this.itemSelected++;
		if (this.itemSelected > 2)
		{
			this.itemSelected = 0;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000223C4 File Offset: 0x000207C4
	private void DecreaseItemSelection()
	{
		this.itemSelected--;
		if (this.itemSelected < 0)
		{
			this.itemSelected = 2;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00022425 File Offset: 0x00020825
	private void UpdateItemSelection()
	{
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0002245C File Offset: 0x0002085C
	public void CollectItem(int item_ID)
	{
		if (this.item[0] == 0)
		{
			this.item[0] = item_ID;
			this.itemSlot[0].texture = this.itemTextures[item_ID];
		}
		else if (this.item[1] == 0)
		{
			this.item[1] = item_ID;
			this.itemSlot[1].texture = this.itemTextures[item_ID];
		}
		else if (this.item[2] == 0)
		{
			this.item[2] = item_ID;
			this.itemSlot[2].texture = this.itemTextures[item_ID];
		}
		else
		{
			this.item[this.itemSelected] = item_ID;
			this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		}
		this.UpdateItemName();
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00022528 File Offset: 0x00020928
	private void UseItem()
	{
		if (this.item[this.itemSelected] != 0)
		{
			if (this.item[this.itemSelected] == 1)
			{
				this.player.stamina = this.player.maxStamina * 2f;
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 2)
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 3)
			{
				Ray ray2 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit2;
				if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit2.transform.position) <= 10f))
				{
					DoorScript component = raycastHit2.collider.gameObject.GetComponent<DoorScript>();
					if (component.DoorLocked)
					{
						component.UnlockDoor();
						component.OpenDoor();
						this.ResetItem();
					}
				}
			}
			else if (this.item[this.itemSelected] == 4)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.bsodaSpray, this.playerTransform.position, this.cameraTransform.rotation);
				this.ResetItem();
				this.player.ResetGuilt("drink", 1f);
				this.audioDevice.PlayOneShot(this.aud_Soda);
			}
			else if (this.item[this.itemSelected] == 5)
			{
				Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem();
						this.CollectItem(4);
					}
					else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem();
						this.CollectItem(1);
					}
					else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
						this.ResetItem();
					}
				}
			}
			else if (this.item[this.itemSelected] == 6)
			{
				Ray ray4 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit4;
				if (Physics.Raycast(ray4, out raycastHit4) && (raycastHit4.collider.name == "TapePlayer" & Vector3.Distance(this.playerTransform.position, raycastHit4.transform.position) <= 10f))
				{
					raycastHit4.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 7)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.alarmClock, this.playerTransform.position, this.cameraTransform.rotation);
				gameObject.GetComponent<AlarmClockScript>().baldi = this.baldiScrpt;
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 8)
			{
				Ray ray5 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit5;
				if (Physics.Raycast(ray5, out raycastHit5) && (raycastHit5.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit5.transform.position) <= 10f))
				{
					raycastHit5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor();
					this.ResetItem();
					this.audioDevice.PlayOneShot(this.aud_Spray);
				}
			}
			else if (this.item[this.itemSelected] == 9)
			{
				Ray ray6 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
				RaycastHit raycastHit6;
				if (this.player.jumpRope)
				{
					this.player.DeactivateJumpRope();
					this.playtimeScript.Disappoint();
					this.player.ResetGuilt("bullying", 1f);
					this.audioDevice.PlayOneShot(this.aud_Scissors);
					this.ResetItem();
				}
				else if (Physics.Raycast(ray6, out raycastHit6) && raycastHit6.collider.name == "1st Prize")
				{
					this.firstPrizeScript.GoCrazy();
					this.player.ResetGuilt("bullying", 1f);
					this.audioDevice.PlayOneShot(this.aud_Shock);
					this.ResetItem();
				}
				else if (Physics.Raycast(ray6, out scissorHitWall) && scissorHitWall.collider.tag == "RippedWall")
				{
					wallHitMeshRenderer = scissorHitWall.collider.gameObject.GetComponent<MeshRenderer>();
					wallHitMeshRenderer.material = rippedWallNoTape;
					scissorHitWall.collider.enabled = false;
					this.player.ResetGuilt("faculty", 1f);
					this.audioDevice.PlayOneShot(this.aud_Scissors);
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 10)
			{
				this.player.ActivateBoots();
				base.StartCoroutine(this.BootAnimation());
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 11)
			{
				base.StartCoroutine(this.Teleporter());
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 12)
			{
				this.player.stamina = this.player.maxStamina * 2f;
				base.StartCoroutine(this.Teleporter());
				this.player.ActivateBoots();
				base.StartCoroutine(this.BootAnimation());
				this.firstPrizeScript.GoCrazy();
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.alarmClock, this.playerTransform.position, this.cameraTransform.rotation);
				gameObject.GetComponent<AlarmClockScript>().baldi = this.baldiScrpt;
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 13)
			{
				this.player.stamina = this.player.maxStamina * 3f;
				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 14)
			{

				this.ResetItem();
			}
			else if (this.item[this.itemSelected] == 15)
			{
				tc.acPower = 100;
				this.ResetItem();
			}
		}
	}
	public AudioClip aud_Scissors;
	public AudioClip aud_Shock;
	public Material rippedWallNoTape;
	private MeshRenderer wallHitMeshRenderer;
	private RaycastHit scissorHitWall;
	// Token: 0x06000977 RID: 2423 RVA: 0x00022B40 File Offset: 0x00020F40
	private IEnumerator BootAnimation()
	{
		float time = 15f;
		float height = 375f;
		Vector3 position = default(Vector3);
		this.boots.gameObject.SetActive(true);
		while (height > -375f)
		{
			height -= 375f * Time.deltaTime;
			time -= Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = -375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.boots.gameObject.SetActive(true);
		while (height < 375f)
		{
			height += 375f * Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = 375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		yield break;
	}

	private IEnumerator Teleporter()
	{
		this.inTeleportation = true;
		this.playerCharacter.enabled = false;
		this.playerCollider.enabled = false;
		int teleports = UnityEngine.Random.Range(12, 16);
		int teleportCount = 0;
		float baseTime = 0.2f;
		float currentTime = baseTime;
		float increaseFactor = 1.1f;
		while (teleportCount < teleports)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				this.Teleport();
				teleportCount++;
				baseTime *= increaseFactor;
				currentTime = baseTime;
			}
			if (this.flipped)
			{
				this.player.height = 6f;
			}
			else
			{
				this.player.height = 4f;
			}
			yield return null;
		}
		this.playerCharacter.enabled = true;
		this.playerCollider.enabled = true;
		this.inTeleportation = false;
		yield break;
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x000236BC File Offset: 0x00021ABC
	private void Teleport()
	{
		this.AILocationSelector.GetNewTarget();
		this.player.transform.position = this.AILocationSelector.transform.position + Vector3.up * this.player.height;
		this.audioDevice.PlayOneShot(this.aud_Teleport);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00022B5B File Offset: 0x00020F5B
	private void ResetItem()
	{
		this.item[this.itemSelected] = 0;
		this.itemSlot[this.itemSelected].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00022B8B File Offset: 0x00020F8B
	public void LoseItem(int id)
	{
		this.item[id] = 0;
		this.itemSlot[id].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00022BB1 File Offset: 0x00020FB1
	private void UpdateItemName()
	{
		this.itemText.text = this.itemNames[this.item[this.itemSelected]];
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00022BD4 File Offset: 0x00020FD4
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red;
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			this.audioDevice.clip = this.aud_MachineQuiet;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2)
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00022CC1 File Offset: 0x000210C1
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00022CD0 File Offset: 0x000210D0
	public void Fliparoo()
	{
		this.flipped = true;
		this.player.height = 6f;
		this.player.fliparoo = 180f;
		this.player.flipaturn = -1f;
		Camera.main.GetComponent<CameraScript>().offset = new Vector3(0f, -1f, 0f);
	}

	// Token: 0x040005F7 RID: 1527
	public CursorControllerScript cursorController;

	// Token: 0x040005F8 RID: 1528
	public PlayerScript player;

	// Token: 0x040005F9 RID: 1529
	public Transform playerTransform;
	
	public CharacterController playerCharacter;

	// Token: 0x04000624 RID: 1572
	public Collider playerCollider;

	// Token: 0x04000625 RID: 1573
	public AILocationSelectorScript AILocationSelector;

	// Token: 0x040005FA RID: 1530
	public Transform cameraTransform;

	// Token: 0x040005FB RID: 1531
	public Camera camera;

	// Token: 0x040005FC RID: 1532
	private int cullingMask;

	// Token: 0x040005FD RID: 1533
	public EntranceScript entrance_0;

	// Token: 0x040005FE RID: 1534
	public EntranceScript entrance_1;

	// Token: 0x040005FF RID: 1535
	public EntranceScript entrance_2;

	// Token: 0x04000600 RID: 1536
	public EntranceScript entrance_3;

	// Token: 0x04000601 RID: 1537
	public GameObject baldiTutor;

	// Token: 0x04000602 RID: 1538
	public GameObject baldi;

	// Token: 0x04000603 RID: 1539
	public BaldiScript baldiScrpt;

	// Token: 0x04000604 RID: 1540
	public AudioClip aud_Prize;

	// Token: 0x04000605 RID: 1541
	public AudioClip aud_PrizeMobile;

	// Token: 0x04000606 RID: 1542
	public AudioClip aud_AllNotebooks;
	
	// Token: 0x04000635 RID: 1588
	public AudioClip aud_Teleport;

	// Token: 0x04000635 RID: 1589
	private bool flipped;

	// Token: 0x04000607 RID: 1543
	public GameObject principal;

	// Token: 0x04000608 RID: 1544
	public GameObject crafters;

	// Token: 0x04000609 RID: 1545
	public GameObject playtime;

	// Token: 0x0400060A RID: 1546
	public PlaytimeScript playtimeScript;

	// Token: 0x0400060B RID: 1547
	public GameObject gottaSweep;

	// Token: 0x0400060C RID: 1548
	public GameObject bully;

	// Token: 0x0400060D RID: 1549
	public GameObject firstPrize;

	// Token: 0x0400060E RID: 1550
	public FirstPrizeScript firstPrizeScript;

	// Token: 0x0400060F RID: 1551
	public GameObject quarter;

	// Token: 0x04000610 RID: 1552
	public AudioSource tutorBaldi;

	// Token: 0x04000611 RID: 1553
	public RectTransform boots;

	// Token: 0x04000612 RID: 1554
	public string mode;

	// Token: 0x04000613 RID: 1555
	public int notebooks;

	// Token: 0x04000614 RID: 1556
	public GameObject[] notebookPickups;

	// Token: 0x04000615 RID: 1557
	public int failedNotebooks;

	// Token: 0x04000616 RID: 1558
	public bool spoopMode;

	// Token: 0x04000617 RID: 1559
	public bool finaleMode;

	// Token: 0x04000618 RID: 1560
	public bool debugMode;

	// Token: 0x04000619 RID: 1561
	public bool mouseLocked;

	// Token: 0x0400061A RID: 1562
	public int exitsReached;

	// Token: 0x0400061B RID: 1563
	public int itemSelected;

	// Token: 0x0400061C RID: 1564
	public int[] item = new int[3];

	// Token: 0x0400061D RID: 1565
	public RawImage[] itemSlot = new RawImage[3];

	// Token: 0x0400061E RID: 1566
	private string[] itemNames = new string[]
	{
		"Nothing",
		"Energy-flavored Zesty Bar",
		"Swinging Door Lock",
		"Principal's Office Keys",
		"BSODA",
		"Quarter",
		"Baldi's Least Favorite Tape",
		"Alarm Clock",
		"WD-NoSquee",
		"Safety Scissors",
		"Big Ol' Boots",
		"Teleportation Teleporter",
		"Belly Bubblin' Banana",
		"Bucket Fruit",
		"Sponge Block",
		"Extra Battery"
	};

	// Token: 0x0400061F RID: 1567
	public TMP_Text itemText;

	// Token: 0x04000620 RID: 1568
	public UnityEngine.Object[] items = new UnityEngine.Object[10];

	// Token: 0x04000621 RID: 1569
	public Texture[] itemTextures = new Texture[10];

	// Token: 0x04000622 RID: 1570
	public GameObject bsodaSpray;

	// Token: 0x04000623 RID: 1571
	public GameObject alarmClock;

	// Token: 0x04000624 RID: 1572
	public TMP_Text notebookCount;

	// Token: 0x04000625 RID: 1573
	public GameObject pauseMenu;

	// Token: 0x04000626 RID: 1574
	public GameObject highScoreText;

	// Token: 0x04000627 RID: 1575
	public GameObject warning;

	// Token: 0x04000628 RID: 1576
	public GameObject reticle;

	// Token: 0x04000629 RID: 1577
	public RectTransform itemSelect;

	// Token: 0x0400062A RID: 1578
	private int[] itemSelectOffset;

	// Token: 0x0400062B RID: 1579
	private bool gamePaused;

	// Token: 0x0400062C RID: 1580
	private bool learningActive;

	// Token: 0x0400062D RID: 1581
	private float gameOverDelay;

	// Token: 0x0400062E RID: 1582
	private AudioSource audioDevice;

	// Token: 0x0400062F RID: 1583
	public AudioClip aud_Soda;

	// Token: 0x04000630 RID: 1584
	public AudioClip aud_Spray;

	// Token: 0x04000631 RID: 1585
	public AudioClip aud_buzz;

	// Token: 0x04000632 RID: 1586
	public AudioClip aud_Hang;

	// Token: 0x04000633 RID: 1587
	public AudioClip aud_MachineQuiet;

	// Token: 0x04000634 RID: 1588
	public AudioClip aud_MachineStart;

	// Token: 0x04000635 RID: 1589
	public AudioClip aud_MachineRev;

	// Token: 0x04000636 RID: 1590
	public AudioClip aud_MachineLoop;

	// Token: 0x04000637 RID: 1591
	public AudioClip aud_Switch;

	// Token: 0x04000638 RID: 1592
	public AudioSource schoolMusic;

	// Token: 0x04000639 RID: 1593
	public AudioSource learnMusic;
	
	public bool inTeleportation;

	public GameObject badSum;

	public AudioClip aud_ReadyOrNot;

	public Image DSCI0000;

	public Color[] dsciLevels;

	public TemperatureControllerScript tc;
}
