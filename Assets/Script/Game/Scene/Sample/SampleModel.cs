using System;
using Core.Common.Model;
using UniRx;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

namespace Game.Scene.Sample
{
    public class SampleModel : IHealth, IDamage, IModel
    {
        readonly int maxHp = 100;
        readonly ReactiveProperty<int> health = new();
        readonly Subject<Unit> healthZero = new();
        readonly Subject<int> healthMax = new();
        public IReactiveProperty<int> OnHealthChanged => health;
        public IObservable<Unit> OnHealthZero => healthZero;
        public IObservable<int> OnHealthMax => healthMax;

        public static SampleModel Create()
        {
            var model = new SampleModel();
            model.Init();
            return model;
        }

        void Init()
        {
            health.Value = maxHp;
        }
        
        public void Heal()
        {
            health.Value += Random.Range(5, 20);
            health.Value = Math.Clamp(health.Value, 0, maxHp);
            if (health.Value >= maxHp)
            {
                healthMax.OnNext(health.Value);
            }
        }

        public void Damage()
        {
            health.Value -= Random.Range(5, 20);
            health.Value = Math.Clamp(health.Value, 0, maxHp);
            if (health.Value <= 0)
            {
                healthZero.OnNext(Unit.Default);
            }
        }

        public void Dispose()
        {
            
        }

        public void Execute()
        {
            
        }
    }
}