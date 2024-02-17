using UnityEngine;

namespace Core.Common.View
{
    public interface IView
    {
        Canvas Canvas { get; }
        void Push();
        void Pop();
        void Open();
        void Close();
    }
}