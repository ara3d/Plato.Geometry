namespace Assets.ClonerExample
{
    /// <summary>
    /// Uses FNV1a hash
    /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
    /// </summary>
    public static class Hasher
    {
        public const ulong FNV_prime = 1099511628211ul;
        public const ulong FNV_offset = 14695981039346656037;

        public static unsafe ulong Hash(byte* data, long size)
        {
            var hash = FNV_offset;
            for (var i = 0; i < size; i++)
            {
                hash ^= data[i];
                unchecked
                {
                    hash *= FNV_prime;
                }
            }
            return hash;
        }

        public static unsafe ulong Hash(ulong val)
            => Hash((byte*)&val, 8);
    }
}