using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.DTOModels.Base;

namespace Templator.DTO.DTOModels
{
   public class Solution : Entity    //класс решения от робота
    {
        public string templateFileName { get; set; } //нужный нам шаблон

        public string Json { get; set; }

        [NotMapped]
        public Dictionary<string, string> Data
        {
            get { return JsonConvert.DeserializeObject<Dictionary<string, string>>(Json); }
            set { Json = JsonConvert.SerializeObject(value); }
        } //данные (метка-значение)
    }
}
