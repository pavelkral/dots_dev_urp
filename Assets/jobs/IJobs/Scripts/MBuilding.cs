using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Building.Jobs
{
    public class MBuilding : MonoBehaviour
    {
        [SerializeField] private int floors;

        [System.Serializable]
        public struct Data
        {
            [SerializeField] private int _tenants;

            public int PowerUsage { get; private set; }

            private Unity.Mathematics.Random _random;

            public Data(MBuilding building)
            {
                _random = new Unity.Mathematics.Random(1);
                _tenants = building.floors * _random.NextInt(20, 500);
                PowerUsage = 0;
                Debug.Log(_tenants);
            }

            public void Update()
            {
                var random = new Unity.Mathematics.Random(1);
                for (var i = 0; i < _tenants; i++)
                {
                    PowerUsage += random.NextInt(12, 24);
                }
            }
        }
    }
}