using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleSpawner spawner;

    /// <summary>
    /// ũ��� ȸ������ �ʱ�ȭ
    /// </summary>
    private void Reset()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    public void Setup(ObstacleSpawner spaner, Vector3 start, Vector3 end)
    {
        this.spawner = spaner;

        Reset();
        StartCoroutine(Process(start, end));
    }

    /// <summary>
    /// ��ֹ��� ��Ȱ��ȭ �� �� ȣ���ϴ� �޼ҵ�
    /// </summary>
    public void OnDie()
    {
        spawner.DeactivateObject(gameObject);
    }

    private IEnumerator Process(Vector3 start, Vector3 end)
    {
        float moveTime = 2f;
        float rotateAngle = Random.Range(10f, 720f) * moveTime;
        //ȸ�� + �̵� ����
        StartCoroutine(TransformEffect.OnRotate(transform, Vector3.zero, Vector3.forward * rotateAngle, moveTime));
        yield return StartCoroutine(TransformEffect.OnMove(transform, start, end , moveTime));

        //ũ�� ��� ����
        StartCoroutine(TransformEffect.OnScale(transform, Vector3.one, Vector3.zero, 0.5f, OnDie));
    }
}
