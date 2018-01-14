using ExperimentHelper.Basic;

namespace ExperimentHelper.Interface
{
    public interface IResultObserable
    {
        void RegisterResultItemObserver(IResultObserver observer);
        void RemoveResultItemObserver(IResultObserver observer);
        void NotifyAllResultItemObservers(ResultItem result);
    }
}
