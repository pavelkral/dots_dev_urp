using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public struct MyJob : IJob
{
    public float a;
    public float b;
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = a + b;
    }
}

// Job adding one to a value
public struct AddOneJob : IJob
{
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = result[0] + 1;
    }
}
//.......................................................................

public struct MyParallelJob : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<float> a;
    [ReadOnly]
    public NativeArray<float> b;
    public NativeArray<float> result;

    public void Execute(int i)
    {
        result[i] = a[i] + b[i];
    }
}

public class JobTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        parallelJobTesting();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        // Debug.Log(result[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void jobTesting()
    {
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

        // Setup the data for job #1
         MyJob jobData = new MyJob();
          jobData.a = 10;
          jobData.b = 10;
           jobData.result = result;

        // Schedule job #1
          JobHandle firstHandle = jobData.Schedule();

        // Setup the data for job #2
          AddOneJob incJobData = new AddOneJob();
          incJobData.result = result;

        // Schedule job #2
          JobHandle secondHandle = incJobData.Schedule(firstHandle);

        // Wait for job #2 to complete
           secondHandle.Complete();

        // All copies of the NativeArray point to the same memory, you can access the result in "your" copy of the NativeArray
          float aPlusB = result[0];
          Debug.Log(aPlusB);
        // Free the memory allocated by the result array
          result.Dispose();

    }
    public void parallelJobTesting()
    {
        NativeArray<float> a = new NativeArray<float>(2, Allocator.TempJob);

        NativeArray<float> b = new NativeArray<float>(2, Allocator.TempJob);

        NativeArray<float> result = new NativeArray<float>(2, Allocator.TempJob);

        a[0] = 1.1f;
        b[0] = 2.2f;
        a[1] = 3.3f;
        b[1] = 4.4f;

        MyParallelJob jobData = new MyParallelJob();
        jobData.a = a;
        jobData.b = b;
        jobData.result = result;

        // Schedule the job with one Execute per index in the results array and only 1 item per processing batch
        JobHandle handle = jobData.Schedule(result.Length, 1);

        // Wait for the job to complete
        handle.Complete();
        Debug.Log(result[0]);
        // Free the memory allocated by the arrays
        a.Dispose();
        b.Dispose();

        result.Dispose();

    }
}
