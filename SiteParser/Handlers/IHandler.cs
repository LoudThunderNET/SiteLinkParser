using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Handlers
{
    /// <summary>
    /// Представляет обработчик данных из контекста парсинга.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Ссылка на следующий обработчик.
        /// </summary>
        IHandler Next { get; set; }

        /// <summary>
        /// Инкапсулирует логику обработки.
        /// </summary>
        /// <param name="context">Контекст парсинга.</param>
        /// <returns>Задача.</returns>
        Task HandleAsync(ParseContext context);
    }
}
