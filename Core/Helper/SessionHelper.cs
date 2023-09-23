using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Core.Helper
{
    public interface ISessionHelper
    {
        bool SetSession<T>(string key, T value);
        T? GetSession<T>(string key);
    }
    public class SessionHelper : ISessionHelper
    {
        private readonly ISession _session;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public bool SetSession<T>(string key, T value)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                _session.SetString(key, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T? GetSession<T>(string key)
        {
            try
            {
                var json = _session.GetString(key);

                if (string.IsNullOrEmpty(json))
                {
                    return default;
                }

                var value = JsonSerializer.Deserialize<T>(json);
                return value;
            }
            catch
            {
                return default;
            }
        }
    }
}
