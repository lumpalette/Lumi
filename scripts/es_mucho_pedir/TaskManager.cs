using Godot;
using System;
using System.Collections.Generic;

namespace PincheItzel.EsMuchoPedir;

public partial class TaskManager : Node
{
	public static void Add(Task task)
	{
		DateTime dateKey = new(task.Date.Year, task.Date.Month, task.Date.Day);

		if (s_instance._taskCalendar.TryGetValue(dateKey, out List<Task> dailyTasks))
		{
			dailyTasks = [];
			s_instance._taskCalendar.Add(dateKey, dailyTasks);
		}

		dailyTasks.Add(task);
	}

	public static void Remove(Task task)
	{
		DateTime dateKey = new(task.Date.Year, task.Date.Month, task.Date.Day);

		if (!s_instance._taskCalendar.TryGetValue(dateKey, out List<Task> dailyTasks))
		{
			return;
		}

		dailyTasks.Remove(task);
	}

	private TaskManager()
	{
		if (s_instance is not null)
		{
			QueueFree();
			return;
		}

		s_instance = this;

		_taskCalendar = [];
	}

	private static TaskManager s_instance;
	private readonly Dictionary<DateTime, List<Task>> _taskCalendar;
}
