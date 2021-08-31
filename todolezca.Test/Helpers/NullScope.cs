using System;
using System.Collections.Generic;
using System.Text;

namespace todolezca.Test.Helpers
{
    class NullScope : IDisposable
    {
        public static NullScope Instance { get; set; } = new NullScope();
        public void Dispose(){ }

        public NullScope() { }
    }
}
