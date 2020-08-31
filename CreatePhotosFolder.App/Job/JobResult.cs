using System.Collections.Generic;
using System.Linq;

namespace CreatePhotosFolder.App.Job
{
    public class JobResult
    {
        private const int _MAX_ERRORS = 10;

        public bool Success { get; }
        public string FailureReason { get; }

        public JobResult()
        {
            Success = true;
        }

        private JobResult(string failureReason)
        {
            Success = false;
            FailureReason = failureReason;
        }

        public JobResult(IEnumerable<string> failureReasons) 
            : this(Flatten(failureReasons))
        {
        }

        public JobResult(string failureDescription, IEnumerable<string> failureReasons = null)
            : this($"{failureDescription}: {Flatten(failureReasons)}")
        {
        }

        private static string Flatten(IEnumerable<string> entries)
        {
            if (entries == null)
                return null;

            return string.Join("\r\n", entries.Take(_MAX_ERRORS));
        }
    }
}
