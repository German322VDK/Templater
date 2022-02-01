using System.Collections.Generic;
using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
    public class MarkValues : Entity
    {
        public virtual ICollection<MarkValue> Values { get; set; } = new List<MarkValue>();
    }
}
