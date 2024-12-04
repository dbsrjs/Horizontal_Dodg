using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class TransformEffect
{
    /// <summary>
    /// �̵�
    /// </summary>
    /// <param name="target">�̵��� ���</param>
    /// <param name="start">���� ��ġ</param>
    /// <param name="end">��ǥ ��ġ</param>
    /// <param name="moveTime">�̵� �ð�</param>
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

        //�̵� ����
        if(action != null) action.Invoke();
    }

    /// <summary>
    /// ȸ��
    /// </summary>
    /// <param name="target">ȸ���� ���</param>
    /// <param name="start">���� ����</param>
    /// <param name="end">��ǥ ����</param>
    /// <param name="rotateTime">ȸ�� �ð�</param>
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
    /// ũ�� ��ȯ
    /// </summary>
    /// <param name="target">ũ�� ��ȯ�� ���</param>
    /// <param name="start">���� ũ��</param>
    /// <param name="end">��ǥ ũ��</param>
    /// <param name="scaleTime">ũ�� ��ȯ �ð�</param>
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
