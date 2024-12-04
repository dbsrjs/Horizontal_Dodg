using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class TransformEffect
{
    /// <summary>
    /// 이동
    /// </summary>
    /// <param name="target">이동할 대상</param>
    /// <param name="start">시작 위치</param>
    /// <param name="end">목표 위치</param>
    /// <param name="moveTime">이동 시간</param>
    public static IEnumerator OnMove(Transform target, Vector3 start, Vector3 end, float moveTime = 1f, UnityAction action = null)
    {
        if(target == null) yield break;

        float percent = 0f;

        while(percent < 1)
        {
            percent += Time.deltaTime / moveTime;

            target.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        //이동 종료
        if(action != null) action.Invoke();
    }

    /// <summary>
    /// 회전
    /// </summary>
    /// <param name="target">회전할 대상</param>
    /// <param name="start">시작 각도</param>
    /// <param name="end">목표 각도</param>
    /// <param name="rotateTime">회전 시간</param>
    public static IEnumerator OnRotate(Transform target, Vector3 start, Vector3 end, float rotateTime=1f, UnityAction action=null)
    {
        if (target == null) yield break;

        float percent = 0f;

        while (percent < 1)
        {
            percent += Time.deltaTime / rotateTime;

            target.rotation = Quaternion.Euler(Vector3.Lerp(start, end, percent)); 

            yield return null;
        }

        if (action != null) action.Invoke();
    }

    /// <summary>
    /// 크기 변환
    /// </summary>
    /// <param name="target">크기 변환할 대상</param>
    /// <param name="start">시작 크기</param>
    /// <param name="end">목표 크기</param>
    /// <param name="scaleTime">크기 변환 시간</param>
    public static IEnumerator OnScale(Transform target, Vector3 start, Vector3 end, float scaleTime = 1f, UnityAction action = null)
    {
        if (target == null) yield break;

        float percent = 0f;

        while (percent < 1)
        {
            percent += Time.deltaTime / scaleTime;

            target.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if (action != null) action.Invoke();
    }
}
