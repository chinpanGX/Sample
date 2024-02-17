using Common.Director;
using Common.Presenter;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scene.Sample
{
    public class SampleSceneDirector : MonoBehaviour, IDirector
    {
        UpdatablePresenter updatablePresenter;

        void Start()
        {
            updatablePresenter = new UpdatablePresenter();
            Push("Title");
            this.UpdateAsObservable().Subscribe(_ => updatablePresenter.Execute()).AddTo(gameObject);
        }

        /// <summary>
        /// 画面遷移
        /// </summary>
        /// <param name="name"></param>
        public void Push(string name)
        {
            IPresenter request = name switch
            {
                "Title" => new SampleTitlePresenter(this, SampleTitleModel.Create(), SampleTitleView.Create()),
                "Sample" => new SamplePresenter(this, SampleModel.Create(), SampleView.Create()),
                _ => null!
            };
            updatablePresenter.Set(request);
        }
    }
}
