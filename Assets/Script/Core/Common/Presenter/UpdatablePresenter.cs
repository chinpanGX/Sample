#nullable enable

namespace Common.Presenter
{
    public class UpdatablePresenter
    {
        IPresenter? presenter = null;
        IPresenter? request = null;

        public void Execute()
        {
            if (request != null)
            {
                presenter?.Dispose();
                presenter = request;
                request = null!;
            }

            presenter?.Execute();
        }

        public void Set(IPresenter presenter)
        {
            request = presenter;
        }

    }
}