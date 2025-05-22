using CAM.Service;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CAM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {

        private readonly ILogger<ApiControllerBase> _logger;
        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            _logger = logger;
        }

        protected Tuple<bool, string> CheckNulity<T>(T entry)
        {
            bool isNull = false;
            List<string> nullProp = new();
            string Nulls = string.Empty;

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (string.IsNullOrWhiteSpace(property.GetValue(entry).ToString()))
                {
                    nullProp.Add(property.Name);
                    isNull = true;
                }
            }

            for (int i = 0;i < nullProp.Count;i++)
            {
                Nulls += $"{nullProp[i]}, ";
            }

            return new(isNull, Nulls);
        }

    }
}
