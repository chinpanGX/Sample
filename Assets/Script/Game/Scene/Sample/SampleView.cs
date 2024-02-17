#nullable enable
using Common.View;
using Core;
using Core.Common.View;
using Game.Common.UI;
using UnityEngine;
using Screen = Common.View.Screen;

namespace Game.Scene.Sample
{
    public class SampleView : MonoBehaviour, IView
    {
        [SerializeField] Canvas? canvas;
        [SerializeField] HealthGage? healthGage;
        [SerializeField] UIButton? damageButton;
        [SerializeField] UIButton? healButton;
        [SerializeField] UIButton? backButton;

        static Screen? Screen => ComponentLocator.GetOrNull<Screen>();
        public Canvas? Canvas => canvas;
        public HealthGage? HealthGage => healthGage;
        public UIButton? DamageButton => damageButton;
        public UIButton? HealButton => healButton;
        public UIButton? BackButton => backButton;

        public static SampleView Create()
        {
            var view = Instantiate(Resources.Load<SampleView>("Prefabs/View/" + nameof(SampleView)));
            return view;
        }

        public void Setup(int health)
        {
            if (healthGage == null) return;
            healthGage.Setup(health);
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
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void RefreshHpGage(int nextValue)
        {
            if (healthGage == null) 
                return;
            
            healthGage.Health = nextValue;
            healthGage.SetGage();
            healthGage.SetHealthText();
        }

        public void TestMaxHp(int maxHp)
        {
            Debug.Log($"HPが最大になりました。{maxHp}");
        }

        public void TestZeroHp()
        {
            Debug.Log("HPがゼロになりました。");
        }
    }
}