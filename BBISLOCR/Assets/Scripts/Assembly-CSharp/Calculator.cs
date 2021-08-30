using System;
using TMPro;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class Calculator : MonoBehaviour
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002126 File Offset: 0x00000326
	public void WriteOnTextField()
	{
		this.DisplayText.text = " " + this.result;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
	public void ClearResult()
	{
		this.result = 0.0;
		this.multiplier = 1.0;
		this.WriteOnTextField();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000216E File Offset: 0x0000036E
	public void Update()
	{
		if (this.result == 31718.0)
		{
			this.codes.PLACEFACE();
		}
		if (this.result == 53045009.0)
		{
			this.codes.GOOSHOES();
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
	public void SaveOperation(string StringTXT)
	{
		this.operation = StringTXT;
		this.tempSave = this.result;
		this.result = 0.0;
		this.multiplier = 1.0;
		this.WriteOnTextField();
		this.DisplayText.text = this.operation;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002200 File Offset: 0x00000400
	public void AddDigit(int number)
	{
		if (this.multiplier == 1.0)
		{
			this.result *= 10.0;
			this.result += (double)number;
		}
		else
		{
			this.result += (double)number * this.multiplier;
			this.multiplier /= 10.0;
		}
		this.WriteOnTextField();
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002277 File Offset: 0x00000477
	public void AddMultiplier()
	{
		this.multiplier = 0.1;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002288 File Offset: 0x00000488
	public void CalculatorsResult()
	{
		string a = this.operation;
		if (!(a == "+"))
		{
			if (!(a == "-"))
			{
				if (!(a == "*"))
				{
					if (a == "/")
					{
						this.result = this.tempSave / this.result;
					}
				}
				else
				{
					this.result = this.tempSave * this.result;
				}
			}
			else
			{
				this.result = this.tempSave - this.result;
			}
		}
		else
		{
			this.result = this.tempSave + this.result;
		}
		if (this.operation == "store")
		{
			this.storeInt = this.tempSave;
		}
		else if (this.operation == "restore")
		{
			this.result = this.storeInt;
		}
		this.operation = "";
		this.multiplier = 1.0;
		this.WriteOnTextField();
	}

	// Token: 0x0400000A RID: 10
	public TMP_Text DisplayText;

	// Token: 0x0400000B RID: 11
	public double result;

	// Token: 0x0400000C RID: 12
	public double tempSave;

	// Token: 0x0400000D RID: 13
	public double multiplier = 1.0;

	// Token: 0x0400000E RID: 14
	public double storeInt;

	// Token: 0x0400000F RID: 15
	public string operation;

	// Token: 0x04000010 RID: 16
	public SecretCodesController codes;
}
