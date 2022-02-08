using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templator.DTO.DTOModels;

namespace Templater.Data
{
    public class TestData
    {
        public static IEnumerable<Solution> Solutions { get; } = new[]
        {
            new Solution{Id=1,Data = { {"FIO","Тимофеев Илья Александрович" }, { "PassportSerial", "6666"},{ "PassportNumber", "999999" } } },
            new Solution{Id=2,Data = { {"FIO","Иванов Иван Иванович" }, { "PassportSerial", "7777"},{ "PassportNumber", "111111" } } },
            new Solution{Id=3,Data = { {"FIO","Александров Александр Александрович" }, { "PassportSerial", "8888"},{ "PassportNumber", "222222" } } },//не совсем понял нужно ли тут добавлять шаблон вообще и как понять id
        };



    }
}
