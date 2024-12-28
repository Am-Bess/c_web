using AppStoregStore.IAbstract;
using AppStoregStore.Models.DTO;

namespace AppStoregStore.Querty
{
    public class MySimpleQuery
    {
        public IEnumerable<StorageDto> GetProducs([Service] IServiceStorage serviceStorage) => serviceStorage.GetPosition();
    }
}
