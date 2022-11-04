namespace BusinessLogic.Web.Contracts
{
    public interface IAssetService
    {
        string GetRobotUrl(string id);
        string GetMonsterUrl(string id);
    }
}