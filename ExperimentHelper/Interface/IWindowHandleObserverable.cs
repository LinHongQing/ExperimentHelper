namespace ExperimentHelper.Interface
{
    public interface IWindowHandleObserverable
    {
        void RegisterWindowHandleObserver(IWindowHandleObserver observer);
        void RemoveWindowHandleObserver(IWindowHandleObserver observer);
        void NotifyAllWindowHandleObservers();
    }
}
