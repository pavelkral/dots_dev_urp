using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Building.Jobs
{
    public class MBuildingJobManager: MonoBehaviour
    {
        [SerializeField] private List<MBuilding> buildings;

        private MbuildingJob _job;
        private NativeArray<MBuilding.Data> _buildingDataArray;

        private void Awake()
        {
            var buildingData = new MBuilding.Data[buildings.Count];
            for (var i = 0; i < buildingData.Length; i++)
            {
                buildingData[i] = new MBuilding.Data(buildings[i]);
            }

            _buildingDataArray = new NativeArray<MBuilding.Data>(buildingData, Allocator.Persistent);

            _job = new MbuildingJob
            {
                BuildingDataArray = _buildingDataArray
            };
        }

        private void Update()
        {
            var jobHandle = _job.Schedule(buildings.Count, 1);
            jobHandle.Complete();
        }

        private void OnDestroy()
        {
            _buildingDataArray.Dispose();
        }
    }
}