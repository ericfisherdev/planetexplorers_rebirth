using System;
using System.Diagnostics;

public class HighStopwatch
{
	public HighStopwatch()
	{
		// System.Diagnostics.Stopwatch provides cross-platform high-resolution timing.
		// No initialization needed -- Stopwatch.GetTimestamp() and Stopwatch.Frequency
		// are always available (hardware or fallback).
	}

	public Int64 Frequency
	{
		get
		{
			return Stopwatch.Frequency;
		}
	}

	public Int64 Value
	{
		get
		{
			return Stopwatch.GetTimestamp();
		}
	}
}
