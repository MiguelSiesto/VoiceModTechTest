using System;

namespace VoiceModFleckExercise.Wrappers
{
	public class ConsoleWrapper : IConsoleWrapper
	{
		public string ReadInput()
		{
			return Console.ReadLine();
		}

		public void SendOutput(string output, bool isLine = true)
		{
			if (isLine)
				Console.WriteLine(output);
			else
				Console.Write(output);
		}
	}
}
