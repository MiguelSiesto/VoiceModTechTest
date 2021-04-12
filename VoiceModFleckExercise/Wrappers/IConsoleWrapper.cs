namespace VoiceModFleckExercise.Wrappers
{
	public interface IConsoleWrapper
	{
		public string ReadInput();

		public void SendOutput(string output, bool isLine = true);
	}
}
