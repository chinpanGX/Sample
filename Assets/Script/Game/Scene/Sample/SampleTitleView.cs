#nullable enable
using Common.View;
using Core;
using Core.Common.View;
using Game.Common.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Screen = Common.View.Screen;

namespace Game.Scene.Sample
{
    public class SampleTitleView : MonoBehaviour, IView
    {
        [SerializeField] Canvas? canvas;
        [SerializeField] UIText? title;
        [SerializeField] UIButton? button;

        static Screen? Screen => ComponentLocator.GetOrNull<Screen>();
        public Canvas? Canvas => canvas;
        public UIButton? Button => button;

        public static SampleTitleView Create()
        {
            var view = Instantiate(Resources.Load<SampleTitleView>("Prefabs/View/" + nameof(SampleTitleView)));
            return view;
        }
        
        public void Setup(string titleText)
        { 
            title.SetTextSafe(titleText);
            button.SetInteractableSafe(false);
        }
        
        public void Push()
        {
            if (Screen != null)
            {
                Screen.Push(this);
            }
        }

        public void Pop()
        {
            if (Screen != null)
            {
                Screen.Pop();
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void ChangeTitle(string text)
        {
            title.SetTextSafe(text);
        }

        public void ActiveChangeButton()
        {
            if (button != null)
            {
                button.SetInteractable(true);
            }
        }

        public void SetButtonText(string text)
        {
            if (button != null)
            {
                button.SetText(text);
            }
        }
    }
}