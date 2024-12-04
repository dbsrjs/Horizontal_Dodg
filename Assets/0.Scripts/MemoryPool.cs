using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MemoryPool
{
    //메모리 풀로 관리되는 오브젝트 정보
    private class PoolItem
    {
        public GameObject gameObject;   //회면에 보이는 실제 게임 오브젝트
        private bool isActive;          //"gameObject"의 활성화 비활성화 정보

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
        
    private int increaseCount = 5;              //오베즉트가 부족할 때 instantiate()로 추가 생성되는 오브젝트 개수
    private int maxCount;                       //현재 리스트에 동록되어 있는 오브젝트 개수
    private int activeCount;                    //현재 게임에 사용되고 있는(활성화) 오브젝트 개수

    private GameObject poolObject;              //오브젝트 풀링에서 관리하는 게임 오브젝트 프로팹
    private List<PoolItem> poolItemList;        //관리되는 모든 오브젝트를 저장하는 리스트

    public int MaxCount => maxCount;            //외부에서 현재 리스트에 등록되어 있는 오브젝트 개수 확인을 위한 프로퍼티
    public int ActiveCount => activeCount;      //외부에서 현재 활성화 되어 있는 오브젝트 개수 확인을 위한 프로퍼티

    //오브젝트가 임시로 보관되는 위치
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
    /// 현재 관리중인 모든 오브젝트를 삭제함.
    /// </summary>
    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for(int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject); 
        }

        poolItemList.Clear();//리스트 초기화
    }

    /// <summary>
    /// poolItemList에 비활성화 상태로 있는 오브젝트 중 하나를 선택해 활성화 하는 메소드
    /// </summary>
    public GameObject ActivatePoolItem(Vector3 postion)
    {
        if(poolItemList == null) return null;

        //현재 생성해서 관리하는 모든 오브젝트 개수와 현재 활성화 상태인 오브젝트 개수 비교
        //모든 오브젝트가 할솽화 상태이면 새로운 오브젝트 필요
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
    /// 사용이 끝난 오브젝트를 비활성화 하는 메소드
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
    /// 활성화 상태인 모든 리스트 요소들을 비활성화 하는 메소드
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