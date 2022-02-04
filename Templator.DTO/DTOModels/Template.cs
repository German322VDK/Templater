using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
    public class Template : Entity
    {
        public string FileName { get; set; }

        //ICollection<string>
        public string JSONKeys { get; set; }
    }
}
