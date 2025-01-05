using AppStore.Models.DTO;

namespace AppStore.IAbstract
{
    public interface IServiceStorage
    {
        bool AddPosition(StorageDto category);
        IEnumerable<StorageDto> GetPosition();
        bool DeletPosition(StorageDto product);
    }
}
