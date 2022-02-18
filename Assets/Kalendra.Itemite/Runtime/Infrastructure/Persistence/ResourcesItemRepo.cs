using System.Collections.Generic;
using System.Linq;
using Kalendra.Itemite.Runtime.Domain.Application;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Persistence
{
    public class ResourcesItemRepo : IItemRepo
    {
        public IEnumerable<Domain.Item> GetRandom(int count)
        {
            var all = Resources.LoadAll<Item>("Items");
            if(all.Length <= count)
                return all.Select(i => i.ToDomain());

            var result = new List<Item>();
            while(result.Count < count)
            {
                var random = all[Random.Range(0, all.Length - 1)];
                if(!result.Contains(random))
                    result.Add(random);
            }

            return result.Select(i => i.ToDomain());
        }
    }
}