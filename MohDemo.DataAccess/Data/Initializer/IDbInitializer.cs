namespace MohDemo.DataAccess.Data.Initializer
{
	public interface IDbInitializer
    {
        void Initialize();

        void SeedData();
    }
}