using System.Data;
using Dapper;

namespace GameCatalogAPI.Helpers
{
    //Dapper knows how to map common types like int, string, DateTime, etc.
    //But types like DateOnly(introduced in .NET 6) are not supported directly by most databases
    //and Dapper doesn’t know how to serialize/deserialize them by default.
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    //You override this to tell Dapper how to read from and write to the database for DateOnly
    {
        //This method is called when Dapper is writing a DateOnly value to the database.
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            //parameter = the database parameter you're assigning a value to (e.g. @ReleaseDate)
            //value = the DateOnly object from your C# model
            parameter.Value = value.ToString("yyyy-MM-dd");
            // You convert the DateOnly into a string like "2024-04-13"
            // and assign that to the parameter.
        }


        //This method is called when Dapper reads a value from the database
        //and needs to convert it into a DateOnly object in your C# entity
        public override DateOnly Parse(object value)
        {
            //🧠 You use a C# switch expression to check the actual runtime type of value.
            return value switch
            {
                //If the value is a string(e.g. "2023-10-12"),
                //use DateOnly.Parse to convert it into a DateOnly.
                string str => DateOnly.Parse(str),
                DateTime dt => DateOnly.FromDateTime(dt),
                _ => throw new DataException($"Cannot convert {value.GetType()} to DateOnly")
            };
        }
    }

}
