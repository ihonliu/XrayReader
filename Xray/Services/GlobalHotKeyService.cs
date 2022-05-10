using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using GlobalHotKey;

namespace Xray.Services
{
    public class GlobalHotKeyService : IDisposable
    {
        private readonly HotKeyManager _hotKeyManager;
        private readonly Dictionary<HotKey, EventHandler> _hotKeys;

        public GlobalHotKeyService()
        {
            _hotKeys = new Dictionary<HotKey, EventHandler>();
            _hotKeyManager = new HotKeyManager();
            _hotKeyManager.KeyPressed += OnHotKeyPressed;
        }

        ~GlobalHotKeyService()
        {
            Dispose();
        }

        private void OnHotKeyPressed(object? sender, KeyPressedEventArgs e)
        {
            if (_hotKeys.ContainsKey(e.HotKey))
            {
                _hotKeys[e.HotKey].Invoke(this, EventArgs.Empty);
            }
        }

        public void UnregisterHotKey(Key key, ModifierKeys modifierKeys)
        {
            _hotKeyManager.Unregister(key, modifierKeys);
        }

        public void RegisterHotKey(Key key, ModifierKeys modifierKeys, EventHandler handler)
        {
            var hotKey = new HotKey(key, modifierKeys);
            if (_hotKeys.ContainsKey(hotKey))
            {
                _hotKeys[hotKey] = handler;
            }
            else
            {
                _hotKeyManager.Register(hotKey);
                _hotKeys.Add(hotKey, handler);
            }
        }

        public void UnregisterAllHotKeys()
        {
            foreach (var hotKey in _hotKeys.Keys)
            {
                _hotKeyManager.Unregister(hotKey);
            }
        }

        public void Dispose()
        {
            UnregisterAllHotKeys();
            _hotKeyManager.Dispose();
        }
    }
}
