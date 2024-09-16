using System;
using System.Threading.Tasks;

namespace Utils
{
    public static class TaskUtils
    {
        public static async Task WaitWhile(Func<bool> condition)
        {
            while (condition())
                await Task.Delay(100);
        }
    }
}
