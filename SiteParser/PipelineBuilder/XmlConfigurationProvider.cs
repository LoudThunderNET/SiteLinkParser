using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SiteParser.PipelineBuilder
{
    public class XmlConfigurationProvider : IConfigurationProvider
    {
        private HandlersCollection _cachedHandlerCollection;
        public HandlersCollection GetConfig()
        {
            const string fileName = "rules.xml";
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Файл {fileName} не найден.");
            }
            using (var stream = new StreamReader(fileName))
            {
                var serializer = new XmlSerializer(typeof(HandlersCollection));
                _cachedHandlerCollection = (HandlersCollection)serializer.Deserialize(stream);
                return _cachedHandlerCollection;
            }
        }

        public PipelineConfigItem GetHandlerConfig(Type handlerType)
        {
            handlerType = handlerType ?? throw new ArgumentNullException("Не указан тип обработчика.");

            _cachedHandlerCollection = _cachedHandlerCollection ?? GetConfig() ?? throw new ApplicationException("Не удалось получить конфигурацию.");

            PipelineConfigItem result;
            result =  _cachedHandlerCollection.Handlers.FirstOrDefault(h => h.Type == handlerType.AssemblyQualifiedName);
            if (result != null)
            {
                return result;
            }

            result =  _cachedHandlerCollection.Handlers.FirstOrDefault(h => h.Type == handlerType.FullName);
            if (result != null)
            {
                return result;
            }

            result =  _cachedHandlerCollection.Handlers.FirstOrDefault(h => h.Type == handlerType.Name);
            if (result != null)
            {
                return result;
            }
            throw new ApplicationException($"Не удалось найти конфигурацию для типа {handlerType.AssemblyQualifiedName}");
        }
    }
}