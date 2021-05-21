using System;
using Xamarin.Forms;

namespace Masonry.Controls
{
    public class HybridWebView : WebView
    {
        private Action<string> _action;

        public void RegisterAction(Action<string> callback)
        {
            _action = callback;
        }

        public void Cleanup()
        {
            _action = null;
        }

        public void InvokeAction(string data)
        {
            if (_action == null || data == null)
            {
                return;
            }
            _action.Invoke(data);
        }
    }
}