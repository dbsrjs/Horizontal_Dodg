using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private AudioClip directionSound;   //방향 전환 할 때 재생할 사운드

    [SerializeField]
    private AudioClip dieSound;         //사망할 때 재생할 사운드

    [SerializeField]
    private Transform left, right;

    [SerializeField]
    private float moveSpeed;   //이동 속도

    [SerializeField]
    private Vector3 moveDirection = Vector3.right;  //이동 방향

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

        //마우스 클릭 or 화면 터치로 방향 전환
        if(Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(directionSound);    //방향 전환 사운드 재생
            moveDirection *= -1f;
        }

        //이동 방향이 오른쪽일 때 오른쪽 끝에 도달하거나 ||
        //이동 방향이 왼쪽일 때 왼쪽 끝에 도달하면 방향 전환
         if((moveDirection == Vector3.right && transform.position.x >= right.position.x) ||
            (moveDirection == Vector3.left && transform.position.x <= left.position.x))
         {
            audioSource.PlayOneShot(directionSound);    //방향 전환 사운드 재생
            moveDirection *= -1f;
            gameController.Score += 2;
         }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            collision.enabled = false;          //플레이어 오브젝트의 충돌체크 비활성화
            spriteRenderer.enabled = false;     //플레이어 오브젝특 보이지 않도록 설정
            dieEffect.Play();                   //사망 이팩트
            audioSource.PlayOneShot(dieSound);  //사망 사운드
            gameController.GameOver();          //사망 처리
        }
    }
}
