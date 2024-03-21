// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");

var workItems = WorkItem.GetWorkItems(10);

var result = await WorkItemHandler.ProcessWorkItems(workItems);

Console.WriteLine($"Total sum is: {result.totalSum}. We have {result.corrupted.Count} corrupted elements. They are: {string.Join(",", result.corrupted)}");



class WorkItem
{
    public string Id { get; set; }
    public int Complexity { get; set; } // random 1 -10
    public bool StateOk { get; set; }


    public static List<WorkItem> GetWorkItems(int count)
    {
        var result = new List<WorkItem>();
        var random = new Random();

        for (int i = 0; i < count; i++)
        {
            result.Add(new WorkItem() { Complexity = random.Next(10), Id = $"{i}", StateOk = i == 5 || i == 9 ? false : true });
        }

        return result;
    }

    public async Task<int> GetResult()
    {
        await Task.Delay(1000);

        Console.WriteLine($"Runs on thread: {Thread.CurrentThread}");
        var result = 10 * Complexity;

        return result;
    }
}

class WorkItemHandler
{
    public static async Task<(int totalSum, List<string> corrupted)> ProcessWorkItems(List<WorkItem> itemsToProcess)
    {
        var time = new Stopwatch();
        time.Start();
        var corrupted = new List<string>();
        var sum = 0;

        var tasks = new List<Task<int>>();

        foreach (var item in itemsToProcess)
        {
            if (item.StateOk == false)
                corrupted.Add(item.Id);
            else
            {
                var a = item.GetResult();
                tasks.Add(a);
            }
        }

        Task.WaitAll(tasks.ToArray());

        foreach (var t in tasks)
        {
            var result = t.Result;

            sum += result;
        }



        time.Stop();
        Console.WriteLine($"{nameof(ProcessWorkItems)} a durat: {time.ElapsedMilliseconds} milisecunde");
        return (sum, corrupted);
    }
}



