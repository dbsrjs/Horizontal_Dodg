using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public static class FadeEffect
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">대상</param>
    /// <param name="start">시작 값</param>
    /// <param name="end">끝 값</param>
    /// <param name="fadeSpeed">속도</param>
    /// <returns></returns>
    public static IEnumerator FadeLoop(TextMeshProUGUI target, float start, float end, float fadeSpeed = 1)
    {
        if (target == null) yield break;

        while (true)
        {
            Color color = target.color;
            color.a = Mathf.Lerp(start, end, Mathf.PingPong(Time.time * fadeSpeed, 1f));
            target.color = color;

            yield return null;
        }
    }

    public static IEnumerator Fade(TextMeshProUGUI target, float start, float end, float fadeTime = 1, UnityAction action = null)
    {
        if (target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / fadeTime;

            Color color = target.color;
            color.a = Mathf.Lerp(start, end, percent);
            target.color = color;

            yield return null;
        }

        if (action != null) action.Invoke();
    }
}
