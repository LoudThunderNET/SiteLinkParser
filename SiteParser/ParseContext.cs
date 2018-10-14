using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser
{
    /// <summary>
    /// Представлет контекст парсинга
    /// </summary>
    public class ParseContext
    {
        /// <summary>
        /// Текущий Uri обработки.
        /// </summary>
        public Uri CurrentUrl { get; set; }

        /// <summary>
        /// Контент страницы.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Ссылки, полученные при парсинге контента и разделенные на две категории: 
        /// подлежащие и не подлежащие дальнейщей обработки.
        /// </summary>
        public Dictionary<bool, List<Uri>> Links { get; set; }

        /// <summary>
        /// Список исключений, возникших в процессе обработки.
        /// </summary>
        public List<Exception> Exceptions { get; set; }

        public ParseContext()
        {
            Reset();
        }

        /// <summary>
        /// Приводит контекс в исходное сосотояние.
        /// </summary>
        public void Reset()
        {
            Links = new Dictionary<bool, List<Uri>>
            {
                { true,  new List<Uri>() },
                { false, new List<Uri>() }
            };
            Exceptions = new List<Exception>();
            Content = null;
            CurrentUrl = null;
        }
    }
}
