namespace EksamensProject.Infrastructure.SQL
{
    public interface IDbInitializer
    {
        void Initialize(EksamensProjectContext context);

    }
}