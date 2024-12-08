using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private AudioClip directionSound;   //���� ��ȯ �� �� ����� ����

    [SerializeField]
    private AudioClip dieSound;         //����� �� ����� ����

    [SerializeField]
    private Transform left, right;

    [SerializeField]
    private float moveSpeed;   //�̵� �ӵ�

    [SerializeField]
    private Vector3 moveDirection = Vector3.right;  //�̵� ����

    private AudioSource audioSource;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem dieEffect;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        dieEffect = GetComponentInChildren<ParticleSystem>();
        dieEffect.Stop();
    }

    private void Update()
    {
        if(gameController.IsGameStart == false || gameController.IsGameOver == true) return;

        //���콺 Ŭ�� or ȭ�� ��ġ�� ���� ��ȯ
        if(Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(directionSound);    //���� ��ȯ ���� ���
            moveDirection *= -1f;
        }

        //�̵� ������ �������� �� ������ ���� �����ϰų� ||
        //�̵� ������ ������ �� ���� ���� �����ϸ� ���� ��ȯ
         if((moveDirection == Vector3.right && transform.position.x >= right.position.x) ||
            (moveDirection == Vector3.left && transform.position.x <= left.position.x))
         {
            audioSource.PlayOneShot(directionSound);    //���� ��ȯ ���� ���
            moveDirection *= -1f;
            gameController.Score += 2;
         }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            collision.enabled = false;          //�÷��̾� ������Ʈ�� �浹üũ ��Ȱ��ȭ
            spriteRenderer.enabled = false;     //�÷��̾� ������Ư ������ �ʵ��� ����
            dieEffect.Play();                   //��� ����Ʈ
            audioSource.PlayOneShot(dieSound);  //��� ����
            gameController.GameOver();          //��� ó��
        }
    }
}
