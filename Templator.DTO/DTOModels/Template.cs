using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
    public class Template : Entity
    {
        public string FileName { get; set; }

        public virtual MarkKeys Keys { get; set; }
    }
}
