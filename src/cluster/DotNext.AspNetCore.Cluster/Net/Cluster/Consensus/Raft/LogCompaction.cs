using System.Threading;
using System.Threading.Tasks;

namespace DotNext.Net.Cluster.Consensus.Raft
{
    /// <summary>
    /// Provides default algorithm of log compaction.
    /// </summary>
    internal static class LogCompaction
    {
        internal static ValueTask ForceIncrementalCompactionAsync(this PersistentState state, CancellationToken token)
        {
            ValueTask result;
            if (token.IsCancellationRequested)
            {
                result = new ValueTask(Task.FromCanceled(token));
            }
            else if (state.CompactionCount > 0L)
            {
                // cancellation may crash the state of the log so it cannot be canceled
                result = state.ForceCompactionAsync(1L, CancellationToken.None);
            }
            else
            {
                result = new ValueTask();
            }

            return result;
        }
    }
}