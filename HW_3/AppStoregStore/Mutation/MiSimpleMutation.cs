using AppStoregStore.IAbstract;
using AppStoregStore.Models.DTO;

namespace AppStoregStore.Mutation
{
    public class MiSimpleMutation
    {
        public bool AddPosition(StorageDto storageDto, [Service] IServiceStorage serviceStorage) => serviceStorage.AddPosition(storageDto); 
        public bool DeletePosition(StorageDto storageDto, [Service] IServiceStorage serviceStorage) => serviceStorage.DeletPosition(storageDto); 
    }
}
