using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Transform left, right;

    [SerializeField]
    private float moveSpeed;   //�̵� �ӵ�

    [SerializeField]
    private Vector3 moveDirection = Vector3.right;//�̵� ����

    private void Update()
    {
        if(gameController.IsGameStart == false) return;

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
            gameController.Score += 2;
         }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
