using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Common.IDCode
{
    public class DisposableAction : IDisposable
    {
        readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
