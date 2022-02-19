using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class ItemPool : MonoBehaviour, IObjectPool<Item>
    {
        [SerializeField] GameObject prefab;
        IObjectPool<Item> pool;

        void Awake()
        {
            AssertPrefabIsItem();

            pool = new ObjectPool<Item>(CreateItem, BorrowItem, RetrieveItem, Debug.LogError);

            void AssertPrefabIsItem()
            {
                if(prefab == null || !prefab.GetComponent<Item>())
                    throw new InvalidOperationException();
            }
        }

        Item CreateItem()
        {
            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
            return instance.GetComponent<Item>();
        }

        static void BorrowItem(Item itemToGive)
        {
            itemToGive.gameObject.SetActive(true);
        }

        static void RetrieveItem(Item itemToGive)
        {
            itemToGive.gameObject.SetActive(false);
        }

        #region IObjectPool implementation
        public Item Get()
        {
            return pool.Get();
        }

        public PooledObject<Item> Get(out Item v)
        {
            return pool.Get(out v);
        }

        public void Release(Item free)
        {
            pool.Release(free);
        }

        public void Clear()
        {
            pool.Clear();
        }

        public int CountInactive => pool.CountInactive;
        #endregion
    }
}