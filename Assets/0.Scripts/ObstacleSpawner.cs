using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private float currentSpawnTime = 2f;    //���� �ֱ�

    [SerializeField]
    private float minSpawnTime = 0.25f;     //�ּ� ���� �ֱ�

    [SerializeField]
    private float spawnTimeDecrease = 0.1f; //���� �ֱ� ���ҷ�

    [SerializeField]
    private float decreaseTime = 5f;        //���� �ֱ� ���� �ֱ�

    [SerializeField]
    private float minX = -2f, maxX = 2f;    //X ����

    [SerializeField]
    private float minY = -2f, maxY = 5.25f; //Y ����

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

            //50%�� Ȯ���� 2 ~ 4�� ��ֹ��� �Ѳ����� �����ȴ�.
            int spawnCount = Random.value > 0.5f ? Random.Range(2, 5) : 1;
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnObject();  
            }
        }

        if (currentSpawnTime <= minSpawnTime) return;

        //decreaseTime �ð����� ���� �ֱ� �ð� ����
        if(Time.time - lastTime > decreaseTime)
        {
            lastTime = Time.time;
            currentSpawnTime -= spawnTimeDecrease;
        }
    }

    /// <summary>
    /// ������Ʈ ����
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
        gameController.Score++; //�÷��̾ ȸ���� ��ֹ��̻���� �� ���� +1
        memoryPool.DeactivatePoolItem(clone);
    }
}
