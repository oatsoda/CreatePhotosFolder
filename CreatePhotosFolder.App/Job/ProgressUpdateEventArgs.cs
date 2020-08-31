namespace CreatePhotosFolder.App.Job
{
    public class ProgressUpdateEventArgs
    {
        public int Percentage { get; }
        public string Message { get; }

        public ProgressUpdateEventArgs(int percentage, string message)
        {
            Percentage = percentage;
            Message = message;
        }
    }
}
