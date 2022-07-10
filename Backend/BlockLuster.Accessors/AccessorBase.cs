using BlockLuster.Accessors.Interfaces;

namespace BlockLuster.Accessors.Accessors
{
    public class AccessorBase : IAccessorBase
    {
        public string TestMe(string input)
        {
            string result = $"{GetType().Name} : {input}";
            Console.WriteLine(result);
            return result;
        }

        internal T UsingDatabaseContext<T>(Func<EntityFramework.DatabaseContext, T> func)
        {
            using var db = new EntityFramework.DatabaseContext();

            try
            {
                return func(db);

            }
            catch(Exception ex)
            {
                // log error
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}