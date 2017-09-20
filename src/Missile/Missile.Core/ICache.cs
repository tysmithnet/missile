namespace Missile.Core
{
    public interface ICache<TKey, TResult>
    {
        TResult Get(TKey key);
        void Set(TKey key, TResult value);
    }

    public class MemoryCache
    {
        
    }
}