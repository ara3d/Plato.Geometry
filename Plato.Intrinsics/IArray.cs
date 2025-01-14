namespace Plato
{
    public interface IArray<T> : IReadOnlyList<T>
    {
        Integer Count { get; }
        T this[Integer n] { get; }
    }
}