using System.Collections.Generic;
using System.Linq;

namespace CreatePhotosFolder.App.Job
{
    public class JobResult
    {
        public bool Success { get; }
        public string OverallDescription { get; }

        public IReadOnlyList<string> Failures { get; }
        public IReadOnlyList<string> Warnings { get; }

        /// <summary>
        /// Success.
        /// </summary>
        public JobResult(bool datesMayBeIncorrect, IReadOnlyList<string> warnings)
        {
            Success = true;
            OverallDescription = datesMayBeIncorrect
                ? "Files moved, but some dates could not be determined"
                : "Files moved successfully";
            Warnings = warnings;
        }

        public JobResult(string failureDescription, IReadOnlyList<string> warnings, IReadOnlyList<string> failureReasons)
        {
            Success = false;
            OverallDescription = $"Failed: {failureDescription}";
            Warnings = warnings;
            Failures = failureReasons;
        }

        public string FlattenFailures(int max) => Flatten(Failures, max);
        public string FlattenWarnings(int max) => Flatten(Warnings, max);

        private static string Flatten(IEnumerable<string> entries, int max)
        {
            if (entries == null)
                return null;

            return string.Join("\r\n", entries.Take(max));
        }
    }
}
