using System.Collections.Generic;
using Templator.DTO.DTOModels.Base;
using Templator.DTO.Models;

namespace Templater.Infrastructure.Interfaces
{
    public interface IStore<T> where T : Entity
    {
        ICollection<T> GetAll();

        T GetById(int id);

        T Add(T item);

        bool Update(T item);

        bool Delete(int id);

        public bool Update(int id, Status status);
    }
}
