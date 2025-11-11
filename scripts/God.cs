using Godot;
using System;
using System.Collections.Generic;
namespace PincheItzel;

public partial class God : Node
{
	private readonly Dictionary<DateTime, List<Task>> _tasksPerDay;
}
