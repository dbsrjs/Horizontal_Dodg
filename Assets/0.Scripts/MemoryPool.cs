using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MemoryPool
{
    //�޸� Ǯ�� �����Ǵ� ������Ʈ ����
    private class PoolItem
    {
        public GameObject gameObject;   //ȸ�鿡 ���̴� ���� ���� ������Ʈ
        private bool isActive;          //"gameObject"�� Ȱ��ȭ ��Ȱ��ȭ ����

        public bool IsActive
        {
            set
            {
                isActive = value;
                gameObject.SetActive(value);
            }
            get => isActive;
        }
    }
        
    private int increaseCount = 5;              //������Ʈ�� ������ �� instantiate()�� �߰� �����Ǵ� ������Ʈ ����
    private int maxCount;                       //���� ����Ʈ�� ���ϵǾ� �ִ� ������Ʈ ����
    private int activeCount;                    //���� ���ӿ� ���ǰ� �ִ�(Ȱ��ȭ) ������Ʈ ����

    private GameObject poolObject;              //������Ʈ Ǯ������ �����ϴ� ���� ������Ʈ ������
    private List<PoolItem> poolItemList;        //�����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ

    public int MaxCount => maxCount;            //�ܺο��� ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ
    public int ActiveCount => activeCount;      //�ܺο��� ���� Ȱ��ȭ �Ǿ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ

    //������Ʈ�� �ӽ÷� �����Ǵ� ��ġ
    private Vector3 tempPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    public MemoryPool(GameObject poolObject)
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    public void InstantiateObjects()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolitem = new PoolItem();

            poolitem.gameObject = GameObject.Instantiate(poolObject);
            poolitem.gameObject.transform.position = tempPosition;
            poolitem.IsActive = false;

            poolItemList.Add(poolitem);
        }
    }

    /// <summary>
    /// ���� �������� ��� ������Ʈ�� ������.
    /// </summary>
    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for(int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject); 
        }

        poolItemList.Clear();//����Ʈ �ʱ�ȭ
    }

    /// <summary>
    /// poolItemList�� ��Ȱ��ȭ ���·� �ִ� ������Ʈ �� �ϳ��� ������ Ȱ��ȭ �ϴ� �޼ҵ�
    /// </summary>
    public GameObject ActivatePoolItem(Vector3 postion)
    {
        if(poolItemList == null) return null;

        //���� �����ؼ� �����ϴ� ��� ������Ʈ ������ ���� Ȱ��ȭ ������ ������Ʈ ���� ��
        //��� ������Ʈ�� �Ҽ�ȭ �����̸� ���ο� ������Ʈ �ʿ�
        if(maxCount == activeCount)
        {
            InstantiateObjects();
        }

        int count = poolItemList.Count;
        for(int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if(poolItem.IsActive = false)
            {
                activeCount++;
                poolItem.gameObject.transform.position = postion;
                poolItem.IsActive = true;

                return poolItem.gameObject;
            }
        }

        return null;
    }

    /// <summary>
    /// ����� ���� ������Ʈ�� ��Ȱ��ȭ �ϴ� �޼ҵ�
    /// </summary>
    public void DeactivatePoolItem(GameObject removeObject)
    {
        if(poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for(int i = 0;i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if(poolItem.gameObject == removeObject)
            {
                activeCount--;

                poolItem.IsActive = false;
                poolItem.gameObject.transform.position = tempPosition;

                return;
            }
        }
    }

    /// <summary>
    /// Ȱ��ȭ ������ ��� ����Ʈ ��ҵ��� ��Ȱ��ȭ �ϴ� �޼ҵ�
    /// </summary>
    public void DeactivateAllPoolItems()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for(int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];
            if(poolItem.gameObject != null && poolItem.IsActive == true)
            {
                poolItem.IsActive = false;
                poolItem.gameObject.transform.position = tempPosition;
            }
        }

        activeCount = 0;
    }
}