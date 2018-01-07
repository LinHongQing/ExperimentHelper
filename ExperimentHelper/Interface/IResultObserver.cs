using ExperimentHelper.Basic;

namespace ExperimentHelper.Interface
{
    public interface IResultObserver
    {
        void UpdateResultItem(ResultItem result);
        void UpdateProcessResult(ResultItem result);
    }
}
