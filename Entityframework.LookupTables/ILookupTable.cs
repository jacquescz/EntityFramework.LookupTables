namespace EntityFramework.LookupTables
{
    public interface ILookupTable<TId> : IIdentity<TId>
        where TId : struct
    {
        string Description { get; set; }
    }
}