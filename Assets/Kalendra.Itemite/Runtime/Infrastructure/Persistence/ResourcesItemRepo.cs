using System.Collections.Generic;
using System.Linq;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Persistence
{
    public class ResourcesItemRepo : IItemRepo, IVisualItemRepo
    {
        IList<Item> all;

        public IList<Domain.Item> GetRandom(int count)
        {
            LoadAll();

            if(all.Count <= count)
                return all.Select(i => i.ToDomain()).ToList();

            var result = new List<Item>();
            while(result.Count < count)
            {
                var random = all[Random.Range(0, all.Count - 1)];
                if(!result.Contains(random))
                    result.Add(random);
            }

            return result.Select(i => i.ToDomain()).ToList();
        }

        public Sprite GetIconOf(string itemId)
        {
            LoadAll();

            return all.SingleOrDefault(item => item.Id == itemId)?.Icon;
        }

        void LoadAll()
        {
            all ??= Resources.LoadAll<Item>("Items");
        }
    }
}