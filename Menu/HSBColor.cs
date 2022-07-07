using System;
using UnityEngine;

namespace ExamplePlugin;

[Serializable]
public struct HSBColor
{
	public float h;

	public float s;

	public float b;

	public float a;

	public HSBColor(float h, float s, float b, float a)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		this.a = a;
	}

	public HSBColor(float h, float s, float b)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		a = 1f;
	}

	public HSBColor(Color col)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		HSBColor hSBColor = FromColor(col);
		h = hSBColor.h;
		s = hSBColor.s;
		b = hSBColor.b;
		a = hSBColor.a;
	}

	public static HSBColor FromColor(Color color)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		HSBColor result = new HSBColor(0f, 0f, 0f, color.a);
		float r = color.r;
		float g = color.g;
		float num = color.b;
		float num2 = Mathf.Max(r, Mathf.Max(g, num));
		if (num2 <= 0f)
		{
			return result;
		}
		float num3 = Mathf.Min(r, Mathf.Min(g, num));
		float num4 = num2 - num3;
		if (num2 > num3)
		{
			if (g == num2)
			{
				result.h = (num - r) / num4 * 60f + 120f;
			}
			else if (num == num2)
			{
				result.h = (r - g) / num4 * 60f + 240f;
			}
			else if (num > g)
			{
				result.h = (g - num) / num4 * 60f + 360f;
			}
			else
			{
				result.h = (g - num) / num4 * 60f;
			}
			if (result.h < 0f)
			{
				result.h += 360f;
			}
		}
		else
		{
			result.h = 0f;
		}
		result.h *= 0.00277777785f;
		result.s = num4 / num2 * 1f;
		result.b = num2;
		return result;
	}

	public static Color ToColor(HSBColor hsbColor)
	{
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		float num = hsbColor.b;
		float num2 = hsbColor.b;
		float num3 = hsbColor.b;
		if (hsbColor.s != 0f)
		{
			float num4 = hsbColor.b;
			float num5 = hsbColor.b * hsbColor.s;
			float num6 = hsbColor.b - num5;
			float num7 = hsbColor.h * 360f;
			if (num7 < 60f)
			{
				num = num4;
				num2 = num7 * num5 / 60f + num6;
				num3 = num6;
			}
			else if (num7 < 120f)
			{
				num = (0f - (num7 - 120f)) * num5 / 60f + num6;
				num2 = num4;
				num3 = num6;
			}
			else if (num7 < 180f)
			{
				num = num6;
				num2 = num4;
				num3 = (num7 - 120f) * num5 / 60f + num6;
			}
			else if (num7 < 240f)
			{
				num = num6;
				num2 = (0f - (num7 - 240f)) * num5 / 60f + num6;
				num3 = num4;
			}
			else if (num7 < 300f)
			{
				num = (num7 - 240f) * num5 / 60f + num6;
				num2 = num6;
				num3 = num4;
			}
			else if (num7 <= 360f)
			{
				num = num4;
				num2 = num6;
				num3 = (0f - (num7 - 360f)) * num5 / 60f + num6;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 0f;
			}
		}
		return new Color(Mathf.Clamp01(num), Mathf.Clamp01(num2), Mathf.Clamp01(num3), hsbColor.a);
	}

	public Color ToColor()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return ToColor(this);
	}

	public override string ToString()
	{
		return "H:" + h + " S:" + s + " B:" + b;
	}

	public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
	{
		float num;
		float num2;
		if (a.b == 0f)
		{
			num = b.h;
			num2 = b.s;
		}
		else if (b.b == 0f)
		{
			num = a.h;
			num2 = a.s;
		}
		else
		{
			if (a.s == 0f)
			{
				num = b.h;
			}
			else if (b.s == 0f)
			{
				num = a.h;
			}
			else
			{
				float num3;
				for (num3 = Mathf.LerpAngle(a.h * 360f, b.h * 360f, t); num3 < 0f; num3 += 360f)
				{
				}
				while (num3 > 360f)
				{
					num3 -= 360f;
				}
				num = num3 / 360f;
			}
			num2 = Mathf.Lerp(a.s, b.s, t);
		}
		return new HSBColor(num, num2, Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
	}

	public static void Test()
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		HSBColor hSBColor = new HSBColor(Color.get_red());
		HSBColor hSBColor2 = hSBColor;
		Debug.Log((object)("red: " + hSBColor2.ToString()));
		hSBColor = new HSBColor(Color.get_green());
		hSBColor2 = hSBColor;
		Debug.Log((object)("green: " + hSBColor2.ToString()));
		hSBColor = new HSBColor(Color.get_blue());
		hSBColor2 = hSBColor;
		Debug.Log((object)("blue: " + hSBColor2.ToString()));
		hSBColor = new HSBColor(Color.get_grey());
		hSBColor2 = hSBColor;
		Debug.Log((object)("grey: " + hSBColor2.ToString()));
		hSBColor = new HSBColor(Color.get_white());
		hSBColor2 = hSBColor;
		Debug.Log((object)("white: " + hSBColor2.ToString()));
		hSBColor = new HSBColor(new Color(0.4f, 1f, 0.84f, 1f));
		hSBColor2 = hSBColor;
		Debug.Log((object)("0.4, 1f, 0.84: " + hSBColor2.ToString()));
		Color val = ToColor(new HSBColor(new Color(0.643137f, 0.321568f, 0.329411f)));
		Debug.Log((object)("164,82,84   .... 0.643137f, 0.321568f, 0.329411f  :" + ((object)(Color)(ref val)).ToString()));
	}
}
