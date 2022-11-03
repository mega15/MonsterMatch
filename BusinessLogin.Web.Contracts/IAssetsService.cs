namespace BusinessLogin.Web.Contracts
{
    public interface IAssetsService
    {
        string GetRobotUrl(string id);
        string GetMonsterUrl(string id);
    }
}