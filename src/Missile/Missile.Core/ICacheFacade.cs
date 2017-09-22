namespace Missile.Core
{
    public interface ICacheFacade
    {
        ICache<TKey, TResult> Get<TKey, TResult>();
    }
}