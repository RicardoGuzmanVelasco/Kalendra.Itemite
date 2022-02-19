using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public enum SpawnStepping
    {
        NoStepping,
        ByAsyncAwait,
        ByCoroutine
    }

    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] float stepSecondsPerSpawn = .5f;
        [SerializeField] SpawnStepping steppingStrategy;
        [SerializeField] int spawningItemsCount = 10;

        readonly IItemRepo repo = new ResourcesItemRepo();

        readonly List<Item> visibleItems = new List<Item>();
        IObjectPool<Item> pool;

        void Awake()
        {
            pool = GetComponent<ItemPool>();
            ItemSpawned += SetupItem;
        }

        void Start()
        {
            SpawnItems(spawningItemsCount);
        }

        public event Action<Item> ItemSpawned = _ => { };

        void SetupItem(Item item)
        {
            visibleItems.Add(item);

            item.transform.Translate(EmptyLocation());

            ShowBySteppingStrategy(item.gameObject);
        }

        async void ShowBySteppingStrategy(GameObject target)
        {
            switch(steppingStrategy)
            {
                case SpawnStepping.NoStepping:
                    target.SetActive(true);
                    break;
                case SpawnStepping.ByAsyncAwait:
                    await SteppedAsyncShow();
                    break;
                case SpawnStepping.ByCoroutine:
                    StartCoroutine(SteppedCoroutineShow());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            async Task SteppedAsyncShow()
            {
                await Task.Delay(CurrentPopulationDelay);
                target.SetActive(true);
            }

            IEnumerator SteppedCoroutineShow()
            {
                yield return new WaitForSeconds((float)CurrentPopulationDelay.TotalSeconds);
                target.SetActive(true);
            }
        }

        #region Spawning in screen
        void SpawnItems(int count)
        {
            var randomItems = repo.GetRandom(count);
            for(var i = 0; i < count; i++)
                SpawnRandomItemByIndex(i);


            void SpawnRandomItemByIndex(int i)
            {
                var index = i < randomItems.Count ? i : Random.Range(0, randomItems.Count);

                var spawn = pool.Get();
                spawn.Inject(randomItems[index]);

                ItemSpawned.Invoke(spawn);
            }
        }

        Vector3 EmptyLocation()
        {
            Vector3 randomLocation;
            do
                randomLocation = new Vector3(Random.Range(-8f, 8f), Random.Range(-5f, 5f), 0);
            while(IsNotFreeLocation(randomLocation));

            return randomLocation;
        }

        bool IsNotFreeLocation(Vector3 location)
        {
            var occupiedLocations = visibleItems.Select(i => i.transform.position);
            return occupiedLocations.Any(occupied => Vector2.Distance(occupied, location) < 2f);
        }

        TimeSpan CurrentPopulationDelay => TimeSpan.FromSeconds((1 + visibleItems.Count) * stepSecondsPerSpawn);
        #endregion
    }
}