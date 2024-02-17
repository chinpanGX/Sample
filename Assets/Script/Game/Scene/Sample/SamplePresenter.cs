#nullable enable
using App.Common.StateMachine;
using Common.Presenter;
using Game.Common.UI;
using UniRx;

namespace Game.Scene.Sample
{
    public class SamplePresenter : IPresenter
    {
        SampleSceneDirector Director { get; set; }
        SampleModel Model { get; set; }
        SampleView View { get; set; }
        StateMachine<SamplePresenter> StateMachine { get; set; }

        public SamplePresenter(SampleSceneDirector director, SampleModel model, SampleView view)
        {
            Director = director;
            Model = model;
            View = view;
            StateMachine = new(this);
            StateMachine.Change<StateInit>();
        }

        public void Dispose()
        {
            Director = null!;
            View.Pop();
            View = null!;
            Model.Dispose();
            Model = null!;
            
            StateMachine.Dispose();
            StateMachine = null!;
        }

        public void Execute()
        {
            StateMachine.Execute();
        }
        
        class StateInit : StateMachine<SamplePresenter>.State
        {
            public override void Begin(SamplePresenter owner)
            {
                var model = owner.Model;
                var view = owner.View;
                view.DamageButton.AddClickListenerSafe(model.Damage);
                view.HealButton.AddClickListenerSafe(model.Heal);
                view.BackButton.AddClickListenerSafe(() =>
                {
                    owner.Director.Push("Title");
                });
            
                view.Setup(model.OnHealthChanged.Value);
                view.Push();

                model.OnHealthChanged.Subscribe(value => view.RefreshHpGage(value));
                
                
                model.OnHealthZero.Subscribe(_ => view.TestZeroHp());

                model.OnHealthMax.Subscribe(maxHp => view.TestMaxHp(maxHp));
            }
        }
    }
}