using AppStoregStore.Models.DTO;

namespace AppStoregStore.IAbstract
{
    public interface IServiceStorage
    {
        bool AddPosition(StorageDto category);
        IEnumerable<StorageDto> GetPosition();
        bool DeletPosition(StorageDto product);
    }
}
