using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private float currentSpawnTime = 2f;    //생성 주기

    [SerializeField]
    private float minSpawnTime = 0.25f;     //최소 생성 주기

    [SerializeField]
    private float spawnTimeDecrease = 0.1f; //생성 주기 감소량

    [SerializeField]
    private float decreaseTime = 5f;        //생성 주기 감소 주기

    [SerializeField]
    private float minX = -2f, maxX = 2f;    //X 범위

    [SerializeField]
    private float minY = -2f, maxY = 5.25f; //Y 범위

    private MemoryPool memoryPool;
    private float lastSpawnTime = 0f;
    private float lastTime = 0f;

    private void Awake()
    {
        memoryPool = new MemoryPool(obstaclePrefab);
    }

    private void Update()
    {
        if(gameController.IsGameStart == false || gameController.IsGameOver == true) return;

        if(Time.time - lastSpawnTime > currentSpawnTime)
        {
            lastSpawnTime = Time.time;

            //50%의 확률로 2 ~ 4개 장애물이 한꺼번에 생성된다.
            int spawnCount = Random.value > 0.5f ? Random.Range(2, 5) : 1;
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnObject();  
            }
        }

        if (currentSpawnTime <= minSpawnTime) return;

        //decreaseTime 시간마다 생성 주기 시간 감소
        if(Time.time - lastTime > decreaseTime)
        {
            lastTime = Time.time;
            currentSpawnTime -= spawnTimeDecrease;
        }
    }

    /// <summary>
    /// 오브젝트 생성
    /// </summary>
    private void SpawnObject()
    {
        Vector3 start = new Vector3(Random.Range(minX, maxX), maxY, 0f);
        Vector3 end = new Vector3(Random.Range(minX, maxX), minY, 0f);

        GameObject clone = memoryPool.ActivatePoolItem(start);
        clone.GetComponent<Obstacle>().Setup(this, start, end);
    }

    public void DeactivateObject(GameObject clone)
    {
        gameController.Score++; //플레이어가 회피한 장애물이사라질 때 점수 +1
        memoryPool.DeactivatePoolItem(clone);
    }
}
