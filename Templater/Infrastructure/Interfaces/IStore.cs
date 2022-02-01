using System.Collections.Generic;
using Templator.DTO.DTOModels.Base;

namespace Templater.Infrastructure.Interfaces
{
    public interface IStore<T> where T : Entity
    {
        ICollection<T> GetAll();

        T GetById(int id);

        T Add(T item);

        bool Update(T item);

        bool Delete(int id);
    }
}
