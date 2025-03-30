using UnityEngine;

#nullable disable
public static class FEasing
{
  public static float EaseInCubic(float start, float end, float value, float ignore = 1f)
  {
    end -= start;
    return end * value * value * value + start;
  }

  public static float EaseOutCubic(float start, float end, float value, float ignore = 1f)
  {
    --value;
    end -= start;
    return end * (float) ((double) value * (double) value * (double) value + 1.0) + start;
  }

  public static float EaseInOutCubic(float start, float end, float value, float ignore = 1f)
  {
    value /= 0.5f;
    end -= start;
    if ((double) value < 1.0)
      return end * 0.5f * value * value * value + start;
    value -= 2f;
    return (float) ((double) end * 0.5 * ((double) value * (double) value * (double) value + 2.0)) + start;
  }

  public static float EaseOutElastic(float start, float end, float value, float rangeMul = 1f)
  {
    end -= start;
    float num1 = 1f;
    float num2 = num1 * 0.3f * rangeMul;
    float num3 = 0.0f;
    if ((double) value == 0.0)
      return start;
    if ((double) (value /= num1) == 1.0)
      return start + end;
    float num4;
    if ((double) num3 == 0.0 || (double) num3 < (double) Mathf.Abs(end))
    {
      num3 = end;
      num4 = num2 * 0.25f * rangeMul;
    }
    else
      num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
    return num3 * Mathf.Pow(2f, -10f * value * rangeMul) * Mathf.Sin((float) (((double) value * (double) num1 - (double) num4) * 6.2831854820251465) / num2) + end + start;
  }

  public static float EaseInElastic(float start, float end, float value, float rangeMul = 1f)
  {
    end -= start;
    float num1 = 1f;
    float num2 = num1 * 0.3f * rangeMul;
    float num3 = 0.0f;
    if ((double) value == 0.0)
      return start;
    if ((double) (value /= num1) == 1.0)
      return start + end;
    float num4;
    if ((double) num3 == 0.0 || (double) num3 < (double) Mathf.Abs(end))
    {
      num3 = end;
      num4 = num2 / 4f * rangeMul;
    }
    else
      num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
    return (float) -((double) num3 * (double) Mathf.Pow(2f, 10f * rangeMul * --value) * (double) Mathf.Sin((float) (((double) value * (double) num1 - (double) num4) * 6.2831854820251465) / num2)) + start;
  }

  public static float EaseInOutElastic(float start, float end, float value, float rangeMul = 1f)
  {
    end -= start;
    float num1 = 1f;
    float num2 = num1 * 0.3f * rangeMul;
    float num3 = 0.0f;
    if ((double) value == 0.0)
      return start;
    if ((double) (value /= num1 * 0.5f) == 2.0)
      return start + end;
    float num4;
    if ((double) num3 == 0.0 || (double) num3 < (double) Mathf.Abs(end))
    {
      num3 = end;
      num4 = num2 / 4f * rangeMul;
    }
    else
      num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
    return (double) value < 1.0 ? (float) (-0.5 * ((double) num3 * (double) Mathf.Pow(2f, 10f * --value) * (double) Mathf.Sin((float) (((double) value * (double) num1 - (double) num4) * 6.2831854820251465) / num2))) + start : (float) ((double) num3 * (double) Mathf.Pow(2f, -10f * rangeMul * --value) * (double) Mathf.Sin((float) (((double) value * (double) num1 - (double) num4) * 6.2831854820251465) / num2) * 0.5) + end + start;
  }

  public static float EaseInExpo(float start, float end, float value, float ignore = 1f)
  {
    end -= start;
    return end * Mathf.Pow(2f, (float) (10.0 * ((double) value - 1.0))) + start;
  }

  public static float EaseOutExpo(float start, float end, float value, float ignore = 1f)
  {
    end -= start;
    return end * (float) (-(double) Mathf.Pow(2f, -10f * value) + 1.0) + start;
  }

  public static float EaseInOutExpo(float start, float end, float value, float ignore = 1f)
  {
    value /= 0.5f;
    end -= start;
    if ((double) value < 1.0)
      return end * 0.5f * Mathf.Pow(2f, (float) (10.0 * ((double) value - 1.0))) + start;
    --value;
    return (float) ((double) end * 0.5 * (-(double) Mathf.Pow(2f, -10f * value) + 2.0)) + start;
  }

  public static float Linear(float start, float end, float value, float ignore = 1f)
  {
    return Mathf.Lerp(start, end, value);
  }

  public static FEasing.Function GetEasingFunction(FEasing.EFease easingFunction)
  {
    switch (easingFunction)
    {
      case FEasing.EFease.EaseInCubic:
        return new FEasing.Function(FEasing.EaseInCubic);
      case FEasing.EFease.EaseOutCubic:
        return new FEasing.Function(FEasing.EaseOutCubic);
      case FEasing.EFease.EaseInOutCubic:
        return new FEasing.Function(FEasing.EaseInOutCubic);
      case FEasing.EFease.EaseInOutElastic:
        return new FEasing.Function(FEasing.EaseInOutElastic);
      case FEasing.EFease.EaseInElastic:
        return new FEasing.Function(FEasing.EaseInElastic);
      case FEasing.EFease.EaseOutElastic:
        return new FEasing.Function(FEasing.EaseOutElastic);
      case FEasing.EFease.EaseInExpo:
        return new FEasing.Function(FEasing.EaseInExpo);
      case FEasing.EFease.EaseOutExpo:
        return new FEasing.Function(FEasing.EaseOutExpo);
      case FEasing.EFease.EaseInOutExpo:
        return new FEasing.Function(FEasing.EaseInOutExpo);
      case FEasing.EFease.Linear:
        return new FEasing.Function(FEasing.Linear);
      default:
        return (FEasing.Function) null;
    }
  }

  public enum EFease
  {
    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,
    EaseInOutElastic,
    EaseInElastic,
    EaseOutElastic,
    EaseInExpo,
    EaseOutExpo,
    EaseInOutExpo,
    Linear,
  }

  public delegate float Function(float s, float e, float v, float extraParameter = 1f);
}
