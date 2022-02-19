using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class ItemPool : MonoBehaviour, IObjectPool<ItemView>
    {
        [SerializeField] GameObject prefab;
        IObjectPool<ItemView> pool;

        void Awake()
        {
            AssertPrefabIsItem();

            pool = new ObjectPool<ItemView>(CreateItem, BorrowItem, RetrieveItem, Debug.LogError);

            void AssertPrefabIsItem()
            {
                if(prefab == null || !prefab.GetComponent<ItemView>())
                    throw new InvalidOperationException();
            }
        }

        ItemView CreateItem()
        {
            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
            return instance.GetComponent<ItemView>();
        }

        static void BorrowItem(ItemView itemToGive)
        {
            itemToGive.gameObject.SetActive(true);
        }

        static void RetrieveItem(ItemView itemToGive)
        {
            itemToGive.gameObject.SetActive(false);
        }

        #region IObjectPool implementation
        public ItemView Get()
        {
            return pool.Get();
        }

        public PooledObject<ItemView> Get(out ItemView v)
        {
            return pool.Get(out v);
        }

        public void Release(ItemView free)
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