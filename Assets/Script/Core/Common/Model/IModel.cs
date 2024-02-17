using System;

namespace Core.Common.Model
{
    public interface IModel : IDisposable
    {
        public void Execute();
    }
}