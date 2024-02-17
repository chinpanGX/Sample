#nullable enable
using App.Common.StateMachine;
using Common.Presenter;
using Cysharp.Threading.Tasks.Triggers;
using Game.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scene.Sample
{
    public class SampleTitlePresenter : IPresenter
    {
        SampleSceneDirector Director { get; set; }
        SampleTitleModel Model { get; set; }
        SampleTitleView View { get; set; }
        StateMachine<SampleTitlePresenter> StateMachine { get; set; }
        
        public SampleTitlePresenter(SampleSceneDirector director, SampleTitleModel model, SampleTitleView view)
        {
            Director = director;
            Model = model;
            View = view;
            
            StateMachine = new StateMachine<SampleTitlePresenter>(this);
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
        
        class StateInit : StateMachine<SampleTitlePresenter>.State
        {
            public override void Begin(SampleTitlePresenter owner)
            {
                var view = owner.View;
                var model = owner.Model;
                view.Setup(model.Title);
                view.Push();

                model.OnFinishInit.Subscribe(stateName =>
                {
                    view.ChangeTitle(stateName);
                    owner.StateMachine.Change<StateUpdate>();
                });

                model.InitAsync().Forget();
            }
        }

        class StateUpdate : StateMachine<SampleTitlePresenter>.State
        {
            public override void Begin(SampleTitlePresenter owner)
            {
                var view = owner.View;
                var model = owner.Model;
                view.ActiveChangeButton();
                
                view.Button.AddClickListenerSafe(() =>
                {
                    owner.Director.Push("Sample");
                });
                
                model.OnTimeChanged.Subscribe(time => view.SetButtonText($"{time}"));
            }
            
            public override void Execute(SampleTitlePresenter owner)
            {
                var model = owner.Model;
                model.Execute();
            }
        }
    }
}