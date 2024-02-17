#nullable enable
using System;
using System.Threading;
using App.Common.StateMachine;
using Core.Common.Model;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Scene.Sample
{
    public class SampleTitleModel : IModel
    {
        readonly ReactiveProperty<int> time = new();
        readonly Subject<string> finishInit = new ();
        readonly CancellationTokenSource cancellationTokenSource = new();
        
        public string Title { get; }
        public string StateName { get;}
        public IReactiveProperty<int> OnTimeChanged => time;
        public IObservable<string> OnFinishInit => finishInit;
        
        
        public static SampleTitleModel Create()
        {
            return new SampleTitleModel();
        }

        SampleTitleModel()
        {
            Title = "タイトル";
            StateName = "初期化";
        }
        
        public void Dispose()
        {
            cancellationTokenSource.Cancel();
        }

        public void Execute()
        {
            DelayAsync(cancellationTokenSource.Token).Forget();
        }

        async UniTask DelayAsync(CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
            Debug.Log("OnUpdate");
            time.Value += 1;
        }

        public async UniTaskVoid InitAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            finishInit.OnNext("更新ステート");
        }
    }
    
}