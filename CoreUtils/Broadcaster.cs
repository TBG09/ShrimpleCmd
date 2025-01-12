using System;
using System.Collections.Generic;

namespace CoreUtils.Broadcaster
{
    public static class Broadcaster
    {
        private static readonly Dictionary<string, Action<object>> _events = new();

        public static void Subscribe(string eventName, Action<object> handler)
        {
            if (!_events.ContainsKey(eventName))
            {
                _events[eventName] = null;
            }
            _events[eventName] += handler;
        }

        public static void Unsubscribe(string eventName, Action<object> handler)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName] -= handler;

                if (_events[eventName] == null)
                {
                    _events.Remove(eventName);
                }
            }
        }

        public static void Broadcast(string eventName, object data = null)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName]?.Invoke(data);
            }
        }

        public static List<string> GetRegisteredEvents()
        {
            return new List<string>(_events.Keys);
        }
    }
}