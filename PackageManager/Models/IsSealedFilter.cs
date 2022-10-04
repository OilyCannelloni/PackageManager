using System.Linq.Expressions;

namespace PackageManager.Models
{
    public enum IsSealedFilter
    {
        ANY,
        SEALED,
        UNSEALED
    }

    static class IsSealedFilterMethods
    {
        public static Expression<Func<Package, bool>> GetFilter(this IsSealedFilter filter)
        {
            switch (filter)
            {
                case IsSealedFilter.ANY: return p => true;
                case IsSealedFilter.SEALED: return p => p.IsSealed;
                case IsSealedFilter.UNSEALED: return p => !p.IsSealed;
                default: return (p) => false;
            }
        }
    }
}
