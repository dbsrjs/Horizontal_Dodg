using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform left, right;
    [SerializeField]
    private float moveSpeed;   //�̵� �ӵ�
    [SerializeField]
    private Vector3 moveDirection = Vector3.right;//�̵� ����

    private void Update()
    {
        //���콺 Ŭ�� or ȭ�� ��ġ�� ���� ��ȯ
        if(Input.GetMouseButtonDown(0))
        {
            moveDirection *= -1f;
        }

        //�̵� ������ �������� �� ������ ���� �����ϰų� ||
        //�̵� ������ ������ �� ���� ���� �����ϸ� ���� ��ȯ
         if((moveDirection == Vector3.right && transform.position.x >= right.position.x) ||
            (moveDirection == Vector3.left && transform.position.x <= left.position.x))
         {
            moveDirection *= -1f;
         }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
