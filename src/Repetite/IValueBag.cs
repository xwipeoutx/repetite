namespace Repetite
{
    public interface IValueBag
    {
        bool TryGet<T>(string key, out T result);
    }
}