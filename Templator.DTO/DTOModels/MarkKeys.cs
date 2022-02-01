using System.Collections.Generic;
using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
    public class MarkKeys : Entity
    {
        public virtual ICollection<MarkKey> Keys { get; set; } = new List<MarkKey>();
    }
}
