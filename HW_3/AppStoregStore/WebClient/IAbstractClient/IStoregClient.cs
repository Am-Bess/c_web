
namespace AppStoregStore.WebClient.IAbstractClient
{
    public interface IStoregClient
    {
        Task<bool> ExistsProsuct(int id);
    }
}
