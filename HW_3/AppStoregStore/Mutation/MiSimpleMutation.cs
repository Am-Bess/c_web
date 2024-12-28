using AppStoregStore.IAbstract;
using AppStoregStore.Models.DTO;

namespace AppStoregStore.Mutation
{
    public class MiSimpleMutation
    {
        public bool AddPosition(StorageDto storageDto, [Service] IServiceStorage serviceStorage) => serviceStorage.AddPosition(storageDto); // Запрос в Графе на добавление позиции.
        public bool DeletePosition(StorageDto storageDto, [Service] IServiceStorage serviceStorage) => serviceStorage.DeletPosition(storageDto); // Запрос в Графе на удаление позиции.
    }
}
