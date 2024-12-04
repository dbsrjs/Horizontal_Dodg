using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private float currentSpawnTime = 2f;    //���� �ֱ�

    [SerializeField]
    private float minX = -2f, maxX = 2f;    //X ����

    [SerializeField]
    private float minY = -2f, maxY = 5.25f; //Y ����

    private MemoryPool memoryPool;
    private float lastSpawnTime = 0f;

    private void Awake()
    {
        memoryPool = new MemoryPool(obstaclePrefab);
    }

    private void Update()
    {
        if(Time.time - lastSpawnTime > currentSpawnTime)
        {
            lastSpawnTime = Time.time;
            SpawnObject();
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
        memoryPool.DeactivatePoolItem(clone);
    }
}
