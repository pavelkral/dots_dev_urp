
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class SimpleJobWaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        float elapsedTime = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation trans, in MoveSpeed moveSpeed, in WaveData waveData) =>
        {
            float zPosition = waveData.amplitude * math.sin(elapsedTime * moveSpeed.Value
                + trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
            //float zPosition = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.Value
            //  );
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).Schedule(); 
    }
}
