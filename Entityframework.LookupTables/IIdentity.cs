namespace EntityFramework.LookupTables
{
    public interface IIdentity<T>
        where T : struct
    {
        T Id { get; set; }
    }
}