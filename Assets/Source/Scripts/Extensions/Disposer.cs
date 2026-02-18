using System;
using System.Collections.Generic;

namespace Extensions
{
    public class Disposer
    {
        private readonly List<IDisposable> _disposables = new ();

        public void Add(IDisposable disposable)
        {
            if (_disposables.Contains(disposable))
            {
                throw new ArgumentException(nameof(disposable));
            }
            
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[i].Dispose();
            }
        }
    }
}