using System;
using System.Collections.Generic;
using System.Linq;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class ItemSpawner : MonoBehaviour
    {
        readonly IItemRepo repo = new ResourcesItemRepo();
        readonly List<Item> visibleItems = new List<Item>();
        IObjectPool<Item> pool;

        void Awake()
        {
            pool = GetComponent<ItemPool>();
        }

        void Start()
        {
            SpawnItems(10);
        }

        public event Action<Item> ItemSpawned = _ => { };

        void SpawnItems(int count)
        {
            var randomItems = repo.GetRandom(count);
            for(var i = 0; i < count; i++)
                SpawnRandomItemByIndex(i);

            void SpawnRandomItemByIndex(int i)
            {
                var index = i < randomItems.Count ? i : Random.Range(0, randomItems.Count);

                var spawn = pool.Get();
                visibleItems.Add(spawn);

                spawn.Inject(randomItems[index]);
                RandomizePlacing(spawn.transform);

                ItemSpawned.Invoke(spawn);
            }
        }

        void RandomizePlacing(Transform item)
        {
            item.Translate(EmptyLocation());
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
    }
}