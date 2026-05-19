using System.Collections.Generic;

namespace HunterAllen.Utility
{
    [System.Serializable]
    public class RandomObjectContainer<T>
    {
        public List<ObjectWithProbability<T>> Objects = new();

        protected float SpawnChanceSum;
        protected float RandomSpawnChance;
        protected float CurrentSpawnChance;
        protected float PreviousSpawnChance;

        public RandomObjectContainer(List<ObjectWithProbability<T>> objects)
        {
            if (objects == null)
            {
                return;
            }
            
            foreach (var obj in objects)
            {
                Objects.Add(obj);
            }
        }

        public bool TryGet(out T t, int seedOffset = 0)
        {
            SpawnChanceSum = 0f;

            foreach (var obj in Objects)
            {
                SpawnChanceSum += obj.Chance;
            }

            RandomSpawnChance = HARandom.Value(seedOffset) * SpawnChanceSum;
            CurrentSpawnChance = 0f;
            PreviousSpawnChance = 0f;

            foreach (var obj in Objects)
            {
                CurrentSpawnChance += obj.Chance;

                if (RandomSpawnChance >= PreviousSpawnChance && RandomSpawnChance < CurrentSpawnChance)
                {
                    t = obj.Obj;
                    return true;
                }

                PreviousSpawnChance = CurrentSpawnChance;
            }

            t = default;
            return false;
        }
        
        public void Remove<J>(J obj) where J : class
        {
            foreach (var o in Objects)
            {
                if ((o.Obj as J) == obj)
                {
                    Objects.Remove(o);
                    return;
                }
            }
        }
    }
}