using System.Collections.Generic;
using Core.Common.View;
using UnityEngine;

namespace Common.View
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] Camera screenCamera;
        readonly Stack<IView> stack = new(0);
        int sortingOrder;

        public void Push(IView view)
        {
            if (!stack.Contains(view))
            {
                stack.Push(view);
            }

            view.Canvas.worldCamera = screenCamera;
            view.Canvas.sortingOrder = ++sortingOrder;
            view.Canvas.transform.SetParent(transform);
        }

        public void Pop()
        {
            if (stack.TryPop(out var view))
            {
                sortingOrder--;
                sortingOrder = Mathf.Max(sortingOrder, 0);
                view.Close();
            }
        }
    }
}