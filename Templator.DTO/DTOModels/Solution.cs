using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
   public class Solution : Entity    //класс решения от робота
    {
        public virtual Template Template { get; set; } //нужный нам шаблон

        public Dictionary<string, string> Data { get; set; } //данные (метка-значение)
    }
}
