using BusinessLogic.Web.Contracts;

namespace BusinessLogic
{
    public class AssetService : IAssetService
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