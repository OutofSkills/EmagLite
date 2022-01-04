using System;

namespace EmagLite.Client.Utilities
{
    public class AppState
    {
        public bool isCartEmpty = true;

        public event Action OnChange;

        public void SetCartItems(bool isEmpty)
        {
            isCartEmpty = isEmpty;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
