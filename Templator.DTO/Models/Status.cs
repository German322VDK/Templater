using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templator.DTO.Models
{
    public enum Status
    {
        //Не проверен
        Unchecked = 0,
        //Распечатано
        Printed = 1,
        //Готово к печати
        ReadyToPrint = 2,
        //Отложено
        Deferred = 3,
        //Забраковано
        Rejected = 4,
        //Закрыто
        Closed = 5
    }
}
