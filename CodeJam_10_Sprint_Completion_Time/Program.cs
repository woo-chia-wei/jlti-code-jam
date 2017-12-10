using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        var sprint = new Sprint(new List<int>() {4, 3, 2, 1, 4, 6}, 3);
        sprint.AddDependencies(new List<int>() {1, 2, 4});
        sprint.AddDependencies(new List<int>() {2, 3, 4});
        sprint.AddDependencies(new List<int>() {4, 3});
        sprint.AddDependencies(new List<int>() {5, 6});
        sprint.AddDependencies(new List<int>() {6, 3});

//        var sprint = new Sprint(new List<int>() {4, 3, 6, 2}, 2);
//        sprint.AddDependencies(new List<int>() {1, 2});
//        sprint.AddDependencies(new List<int>() {2, 3});
//        sprint.AddDependencies(new List<int>() {3, 1});

        var completionDays = sprint.ComputeCompletionDays();
        Console.WriteLine(completionDays == -1 ? "Infeasible" : $"Minimum work days required: {Convert.ToString(completionDays)}");
    }
}

public class Task
{
    public string Name { get; set; }
    public int RequiredDays { get; set; }
    public int RemainingDays { get; set; }
    public List<Task> Dependencies { get; set; } = new List<Task>();
    public Worker Worker { get; set; }
}

public class Worker
{
    public string Name { get; set; }
    public Task Task { get; set; }
}

public class Sprint
{
    public List<Task> Tasks { get; set; }
    public List<Worker> Workers { get; set; }

    public Sprint(List<int> completionDays, int workerCount)
    {
        Tasks = new List<Task>();
        var count = 1;

        foreach (var day in completionDays)
        {
            Tasks.Add(new Task()
            {
                Name = Convert.ToString(count),
                RequiredDays = day,
                RemainingDays = day,
            });

            count++;
        }

        Workers = Enumerable.Range(1, workerCount).Select(x => new Worker() {Name = $"W{x}"}).ToList();
    }

    public void AddDependencies(List<int> dependencies)
    {
        if (dependencies.Count <= 1) return;

        var targetTask = Tasks.First(x => x.Name == Convert.ToString(dependencies[0]));

        dependencies.RemoveAt(0);

        foreach (var dependent in dependencies)
            targetTask.Dependencies.Add(Tasks.First(x => x.Name == Convert.ToString(dependent)));
    }

    public int ComputeCompletionDays()
    {
        var day = 0;
        var changes = false;
        var previousWorks = new List<string>();
        var logs = new List<string>();

        while (true)
        {
            // Print logs
            var log1 = GetLogOfRemainingDays();
            var log2 = GetLogOfPreviousWorks(previousWorks);
            logs.Add($"{log1} {(string.IsNullOrEmpty(log2) ? "" : " | Workers: " + log2)}");
            previousWorks = new List<string>();

            // Mission is accomplished, return minimum days
            if (GetTotalRemainingDays() == 0)
            {
                PrintLogs(logs);
                return day;
            }

            // Iterate through tasks, check dependencies before assign worker to work
            var executableTasks = new List<Task>();
            foreach (var task in Tasks)
            {
                if (task.Dependencies.All(x => x.RemainingDays == 0) && task.RemainingDays > 0)
                    executableTasks.Add(task);
            }

            // Iterate through executable tasks, assign worker if available
            foreach (var task in executableTasks)
            {
                if (task.Worker == null)
                {
                    if(AllWorkersAreBusy()) continue;
                    SignUpTask(task);
                }

                previousWorks.Add($"{task.Worker.Name}->{task.Name}");
                task.RemainingDays--;
                changes = true;
            }

            //Sign off task if task is completed
            foreach (var task in executableTasks)
                if (task.RemainingDays == 0) SignOffTask(task);

            // Not possible to complete
            if (!changes)
            {
                PrintLogs(logs);
                return -1;
            }

            // Update flags
            day++;
            changes = false;
        }
    }

    private int GetTotalRemainingDays()
    {
        return Tasks.Sum(x => x.RemainingDays);
    }

    private string GetLogOfRemainingDays()
    {
        return string.Join(", ", Tasks.Select(x => x.RemainingDays));
    }

    private string GetLogOfPreviousWorks(List<string> previousWorks)
    {
        previousWorks.Sort();
        return string.Join(", ", previousWorks);
    }

    private void PrintLogs(List<string> logs)
    {
        var count = logs.Count();
        var numOfDigits = Convert.ToString(count).Length;
        var finalLogs = logs.Select((x, index) => $"Day {(index).ToString("D" + Convert.ToString(numOfDigits))} : {x}");

        Console.WriteLine(string.Join(Environment.NewLine, finalLogs));
        Console.WriteLine();
    }

    private bool AllWorkersAreBusy()
    {
        return Workers.All(x => x.Task != null);
    }

    private void SignUpTask(Task task)
    {
        var worker = Workers.First(x => x.Task == null);
        task.Worker = worker;
        worker.Task = task;
    }

    private void SignOffTask(Task task)
    {
        var worker = task.Worker;
        task.Worker = null;
        worker.Task = null;
    }

}

