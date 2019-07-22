using System;
using System.Collections.Generic;

namespace Ultz.Logger
{
    public class CompoundDisposable : IDisposable
    {
        public List<IDisposable> OtherDisposables { get; set; } = new List<IDisposable>();
        
        public void Dispose()
        {
            OtherDisposables.ForEach(x => x.Dispose());
        }
    }
}