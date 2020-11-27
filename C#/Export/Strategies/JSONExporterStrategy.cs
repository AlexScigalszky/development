using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Example.Models;

namespace Example.Services.Export.Strategies
{
    public class JSONExporterStrategy : IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string GetExtension()
        {
            logger.Info("getExtension");
            return @"json";
        }

        public string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            if (plantillas.Count() == 0)
                return "[]";
            List<ExpandoObject> listado = new List<ExpandoObject>();
            // Recorrer todas las plantillas
            foreach (IPlantillaCompuestaExportable pc in plantillas)
            {
                dynamic item = new ExpandoObject();
                item.Codigo = pc.idTemporal;
                item.Latitud = pc.latitud;
                item.Longitud = pc.longitud;
                item.Accuracy = pc.accuracy;
                item.Altitud = pc.altitud;
                item.AltitudAccuracy = pc.altitudAccuracy;
                item.Fecha = pc.fechaHora.ToString(Constants.Formatos.FECHA);
                item.Hora = pc.fechaHora.ToString(Constants.Formatos.HORA);
                item.Nombre = pc.descripcion;
                item.Descripción = pc.criterioDeAgrupacion;
                listado.Add(item);
            }
            return JsonConvert.SerializeObject(new { listado }, Formatting.None);
        }

        public string Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}, {1}", plantilla, instancias.Count());
            if (instancias.Count() == 0)
                return "[]";
            List<ExpandoObject> listado = new List<ExpandoObject>();
            // Recorrer todas las plantillas
            foreach (IInstanciaPlantillaCompuestaExportable ipc in instancias)
            {
                logger.Info("Agregando item");
                dynamic item = new ExpandoObject();
                item.Codigo = ipc.idTemporal;
                item.Latitud = ipc.latitud;
                item.Longitud = ipc.longitud;
                item.Accuracy = ipc.accuracy;
                item.Altitud = ipc.altitud;
                item.AltitudAccuracy = ipc.altitudAccuracy;
                item.Fecha = ipc.fechaHora.ToString(Constants.Formatos.FECHA);
                item.Hora = ipc.fechaHora.ToString(Constants.Formatos.HORA);
                foreach (IConsignaExportable consigna in plantilla.consignas)
                {
                    // Buscar la instancia que corresponde con ps
                    IInstanciaPlantillaSimpleExportable ip = ipc.getInstanciaByConsigna(consigna);
                    if (ip == null)
                        (item as IDictionary<string, object>).Add(consigna.descripcion, string.Empty);
                    else
                        (item as IDictionary<string, object>).Add(consigna.descripcion, ip.convertToData(" - "));
                }
                listado.Add(item);
                logger.Info("Item agregado");
            }
            logger.Info("Listado con {1} elementos", listado.Count);
            return JsonConvert.SerializeObject(new { listado }, Formatting.None);
        }
    }
}