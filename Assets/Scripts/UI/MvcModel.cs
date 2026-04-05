using System;

namespace Project.UI
{
    public class MvcModel
    {
        public int Count { get; private set; }

        public event Action OnNewCount;

        public void SetCount(int value)
        {
            Count = value;
            OnNewCount?.Invoke();
        }

        public void Reset()
        {
            Count = default;
            OnNewCount?.Invoke();
        }
    }
}