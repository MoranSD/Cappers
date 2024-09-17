using System.Collections.Generic;

namespace Utilities.BehaviourTree
{
    public class BehaviourTreeBlackBoard
    {
        private Dictionary<string, object> data;

        public BehaviourTreeBlackBoard()
        {
            data = new Dictionary<string, object>();
        }

        public void SetValue(string key, object value) => data[key] = value;
        public void RemoveValue(string key) => data.Remove(key);
        public bool HasValue(string key) => data.ContainsKey(key);
        public object GetValue(string key) => data[key];
    }
}