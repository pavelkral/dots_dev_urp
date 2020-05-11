using Unity.Collections;
using Unity.Jobs;

namespace Building.Jobs
{

    public struct MbuildingJob : IJobParallelFor
{
    public NativeArray<MBuilding.Data> BuildingDataArray;

    public void Execute(int index)
    {
        var data = BuildingDataArray[index];
        data.Update();
        BuildingDataArray[index] = data;
    }
}
}