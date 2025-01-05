using AppStore.IAbstract;
using AppStore.Models.DTO;

namespace AppStore.Querty
{
    public class MySimpleQuery
    {
        public IEnumerable<StorageDto> GetProducs([Service] IServiceStorage serviceStorage) => serviceStorage.GetPosition();
    }
}
