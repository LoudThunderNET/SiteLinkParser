using SiteParser.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteParser.Parser
{
    /// <summary>
    /// Преставляет интерфейс парсера сайтов.
    /// </summary>
    public interface ISiteParser
    {
        /// <summary>
        /// Конвейр, состоящий из обтельный обработчиков сайта.
        /// Каждый обработчик выполняет определенную операцию.
        /// После выполнения обработки, обработчик может вызвать следующий 
        /// обработчик или принять решение о прерывании дальнейшей обработки.
        /// </summary>
        IHandler PipelineHandler { get; set; }

        /// <summary>
        /// Выполняет разбор сайта по заданной ссылке.
        /// </summary>
        /// <param name="startUri">Начальный uri</param>
        /// <returns>Список ссылок, полученных в результате прсинга сайта.</returns>
        Task<List<Uri>> ParseAsync( Uri startUri);
    }
}