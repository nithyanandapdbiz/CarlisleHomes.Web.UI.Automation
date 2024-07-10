using CarlisleHomes.Web.UI.Automation.Framework.Base;
using CarlisleHomes.Web.UI.Automation.Tests.Data.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlisleHomes.Web.UI.Automation.Tests.Data.Deserializer
{
    public static class DataDeserializer
    {
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;

        public static User GetUserByType(string userType)
        {
            if (DriverContext.Driver.Url.Contains("test.secure") || DriverContext.Driver.Url.Contains("test.rem") || DriverContext.Driver.Url.Contains("test.max"))
            {
                string json = ReadJsonFile(@".\Data\JSON\UserDetails.json");
                var users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json);
                return users.ContainsKey(userType) ? users[userType] : null;
            }
            else
            {
                string json = ReadJsonFile(@".\Data\JSON\UserDetails_SIT.json");
                var users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json);
                return users.ContainsKey(userType) ? users[userType] : null;
            }
        }

        private static string ReadJsonFile(string relativePath)
        {
            string fullPath = Path.GetFullPath(Path.Combine(basePath, relativePath));
            return File.ReadAllText(fullPath);
        }
    }
}