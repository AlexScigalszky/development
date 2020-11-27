using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Example.Models;
using NLog;

namespace Example.Services.Export.Strategies
{
    public class CSVExporterStrategy : GenericExporterStrategy, IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region constantes
        private static string SEPARADOR = ";";
        #endregion

        public virtual string GetExtension()
        {
            logger.Info("getExtension");
            return @"csv";
        }

        public virtual string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            return export(plantillas, SEPARADOR);
        }

        public string export(IEnumerable<IPlantillaCompuestaExportable> plantillas, string separador)
        {
            logger.Info("export {0}, {1}", Utilities.Serializer.Serialize2JSON(plantillas), Utilities.Serializer.Serialize2JSON(separador));
            List<string> rows = new List<string>();
            if (plantillas.Count() == 0)
                return "";
            // Carga de los datos
            List<string> titulos = new List<string>();
            titulos.Add("Codigo");
            titulos.Add("Latitud");
            titulos.Add("Longitud");
            titulos.Add("Accuracy");
            titulos.Add("Altitud");
            titulos.Add("AltitudAccuracy");
            titulos.Add("Fecha");
            titulos.Add("Hora");
            titulos.Add("Nombre");
            titulos.Add("Descripción");
            rows.Add(String.Join(separador, titulos));
            // Carga de datos
            List<string> row;
            // Recorrer todas las plantillas
            foreach (IPlantillaCompuestaExportable pc in plantillas)
            {
                // para cada plantilla compuesta
                row = new List<string>();
                row.Add(pc.idTemporal + "");
                row.Add(pc.latitud + "");
                row.Add(pc.longitud + "");
                row.Add(pc.accuracy + "");
                row.Add(pc.altitud + "");
                row.Add(pc.altitudAccuracy + "");
                row.Add(pc.fechaHora.ToString(Constants.Formatos.FECHA) + "");
                row.Add(pc.fechaHora.ToString(Constants.Formatos.HORA) + "");
                row.Add(pc.descripcion);
                row.Add(pc.criterioDeAgrupacion);
                rows.Add(String.Join(separador, row));
            }
            return String.Join(Environment.NewLine, rows);
        }

        public virtual String Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}, {1}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias));
            return export(plantilla, instancias, SEPARADOR);
        }

        protected string export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias, string separador)
        {
            logger.Info("export {0}, {1}, {2}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias), separador);
            List<string> rows = new List<string>();
            if (instancias.Count() == 0)
                return "";
            // Carga de los datos
            List<string> titulos = new List<string>();
            titulos.Add("id");
            titulos.Add("Latitud");
            titulos.Add("Longitud");
            titulos.Add("Accuracy");
            titulos.Add("Altitud");
            titulos.Add("AltitudAccuracy");
            titulos.Add("Codigo");
            titulos.Add("Fecha");
            titulos.Add("Hora");
            foreach (IConsignaExportable consigna in plantilla.consignas)
            {
                titulos.Add(consigna.descripcion);
            }
            rows.Add(String.Join(separador, titulos));
            // Carga de datos
            List<string> row;
            // Recorrer todas las instancias
            foreach (IInstanciaPlantillaCompuestaExportable ipc in instancias)
            {
                // para cada instancia compuesta, se busca el dato correspondiente a cada plantilla simple (titulos)
                row = new List<string>();
                row.Add(ipc.id + "");
                row.Add(ipc.latitud + "");
                row.Add(ipc.longitud + "");
                row.Add(ipc.accuracy + "");
                row.Add(ipc.altitud + "");
                row.Add(ipc.altitudAccuracy + "");
                row.Add(ipc.idTemporal + "");
                row.Add(ipc.fechaHora.ToString(Constants.Formatos.FECHA));
                row.Add(ipc.fechaHora.ToString(Constants.Formatos.HORA));
                foreach (IConsignaExportable consigna in plantilla.consignas)
                {
                    // Buscar la instancia que corresponde con ps
                    IInstanciaPlantillaSimpleExportable ip = ipc.getInstanciaByConsigna(consigna);
                    if (ip == null)
                        row.Add("-");
                    else
                        row.Add(ip.convertToData(separador));
                }
                rows.Add(String.Join(separador, row));
            }
            return String.Join(Environment.NewLine, rows);
        }
    }
}