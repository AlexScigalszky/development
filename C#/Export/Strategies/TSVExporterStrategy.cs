using Example.Models;
using NLog;
using System;
using System.Collections.Generic;

namespace Example.Services.Export.Strategies
{
    public class TSVExporterStrategy : CSVExporterStrategy, IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string SEPARADOR = Convert.ToString(Convert.ToChar(9));

        public override string GetExtension()
        {
            logger.Info("getExtension");
            return @"tsv";
        }

        public override string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            return export(plantillas, SEPARADOR);
        }

        public override string Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias));
            return export(plantilla, instancias, SEPARADOR);
        }

    }
}