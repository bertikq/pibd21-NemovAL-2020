using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.Enums
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum OrderStatus
    {
        Принят = 5,
        Выполняется = 0,
        Готов = 2,
        Оплачен = 3,
        ТребуютсяКомпоненты = 4
    }
}

