using System.Collections.Generic;

namespace PincheItzel.EsMuchoPedir;

public class TaskCollection
{
	public Task this[int index] => _tasks[index];



	private readonly List<Task> _tasks = [];
}