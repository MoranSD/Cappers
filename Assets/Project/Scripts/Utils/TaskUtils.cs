using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public static class TaskUtils
    {
        public static async Task WaitWhile(Func<bool> condition, CancellationToken token)
        {
            while (true)
            {
                if (condition() == false) return;

                if(token.IsCancellationRequested) return;

                await Task.Delay(100);
            }
        }
    }
}
