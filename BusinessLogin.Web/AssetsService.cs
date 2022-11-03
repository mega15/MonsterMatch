using BusinessLogin.Web.Contracts;
using System.Reflection.PortableExecutable;

namespace BusinessLogic
{
    public class AssetsService : IAssetsService
    {
        public string GetRobotUrl(string id)
        {
            return $"https://robohash.org/{id}";
        }

        public string GetMonsterUrl(string id)
        {
            return $"https://robohash.org/{id}?set=set2";
        }
    }
}