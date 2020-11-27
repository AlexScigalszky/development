using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Example.DAL;
using Example.Models;

namespace Example.Services.Export
{
    public class ExportService : GenericService, IExportService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Dictionary<Constants.FORMATS_AVALIBLES, IExportStrategy> _ExporterStrategies = new Dictionary<Constants.FORMATS_AVALIBLES, IExportStrategy>();
        IExportStrategy _ExporterStrategy;

        public ExportService(IUnitOfWork context, IUserIdGetter UserIdGetter) : base(context, UserIdGetter)
        {
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.TSV, new Strategies.TSVExporterStrategy());
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.CSV, new Strategies.CSVExporterStrategy());
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.EXCEL, new Strategies.ExcelExporterStrategy());
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.JSON, new Strategies.JSONExporterStrategy());
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.KML, new Strategies.KMLExporterStrategy());
            _ExporterStrategies.Add(Constants.FORMATS_AVALIBLES.XML, new Strategies.XMLExporterStrategy());
            SetFormat(Constants.FORMATS_AVALIBLES.CSV);
        }

        public MemoryStream Export(int idPlantillaCompuesta, Constants.FORMATS_AVALIBLES format)
        {
            logger.Info("Export {0}, {1}", idPlantillaCompuesta, format);
            // buscar las datos
            IEnumerable<Models.InstanciaPlantillaCompuesta> instancias = context.InstanciasPlantillasCompuestas.GetWithPlantillaAndPosicionByPlantilla(idPlantillaCompuesta);
            PlantillaCompuesta plantilla = context.PlantillasCompuestas.Get(idPlantillaCompuesta);
            if (plantilla == null)
            {
                return null;
            }
            // exportar al formato
            SetFormat(format);
            return Export(plantilla, instancias);
        }

        public void SetFormat(Constants.FORMATS_AVALIBLES format)
        {
            logger.Info("Export {0}", format);
            _ExporterStrategy = _ExporterStrategies[format];
        }

        public MemoryStream Export(IEnumerable<PlantillaCompuesta> plantillas)
        {
            logger.Info("Export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            var stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            string datos = _ExporterStrategy.Export(plantillas);
            writer.Write(datos);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public MemoryStream Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("Export {0}, {1}", plantilla, instancias.Count());
            var stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            logger.Info("Strategy selected {0}", _ExporterStrategy);
            string datos = _ExporterStrategy.Export(plantilla, instancias);
            writer.Write(datos);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public string GetExtension(Constants.FORMATS_AVALIBLES format)
        {
            logger.Info("getExtension {0}", format);
            return _ExporterStrategy.GetExtension();
        }


    }
}