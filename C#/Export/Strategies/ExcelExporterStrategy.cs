using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Example.Models;
using NLog;

namespace Example.Services.Export.Strategies
{
    public class ExcelExporterStrategy : GenericExporterStrategy, IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string GetExtension()
        {
            logger.Info("getExtension");
            return @"xls";
        }

        public string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            if (plantillas.Count() == 0)
                return "";
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`1px`b>");
            str.Append("<tr>");
            str.Append("<td><b>Codigo</b></td>");
            str.Append("<td><b>Latitud</b></td>");
            str.Append("<td><b>Longitud</b></td>");
            str.Append("<td><b>Accuracy</b></td>");
            str.Append("<td><b>Altitud</b></td>");
            str.Append("<td><b>AltitudAccuracy</b></td>");
            str.Append("<td><b>Fecha</b></td>");
            str.Append("<td><b>Hora</b></td>");
            str.Append("<td><b>Nombre</b></td>");
            str.Append("<td><b>Descripción</b></td>");
            str.Append("</tr>");
            // Carga de datos
            // Recorrer todas las instancias
            foreach (IPlantillaCompuestaExportable pc in plantillas)
            {
                str.Append("<tr>");
                // para cada instancia compuesta, se busca el dato correspondiente a cada plantilla simple (titulos)
                str.Append("<td>");
                str.Append(pc.idTemporal);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.latitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.longitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.accuracy);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.altitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.altitudAccuracy);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.fechaHora.ToString(Constants.Formatos.FECHA));
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.fechaHora.ToString(Constants.Formatos.HORA));
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.descripcion);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pc.criterioDeAgrupacion);
                str.Append("</td>");

                str.Append("</tr>");
            }
            str.Append("</table>");
            return str.ToString();
        }

        public String Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}, {1}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias));
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`1px`b>");
            str.Append("<tr>");
            str.Append("<td><b>Id</b></td>");
            str.Append("<td><b>Latitud</b></td>");
            str.Append("<td><b>Longitud</b></td>");
            str.Append("<td><b>Accuracy</b></td>");
            str.Append("<td><b>Altitud</b></td>");
            str.Append("<td><b>AltitudAccuracy</b></td>");
            str.Append("<td><b>Codigo</b></td>");
            str.Append("<td><b>Fecha</b></td>");
            str.Append("<td><b>Hora</b></td>");
            // Carga de los datos
            foreach (IConsignaExportable consigna in plantilla.consignas)
            {
                str.Append("<td><b>");
                str.Append(consigna.descripcion);
                str.Append("</b></td>");
            }
            str.Append("</tr>");
            // Carga de datos
            // Recorrer todas las instancias
            foreach (IInstanciaPlantillaCompuestaExportable ipc in instancias)
            {
                str.Append("<tr>");
                // para cada instancia compuesta, se busca el dato correspondiente a cada plantilla simple (titulos)
                str.Append("<td>");
                str.Append(ipc.id);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.latitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.longitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.accuracy);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.altitud);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.altitudAccuracy);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.idTemporal);
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.fechaHora.ToString(Constants.Formatos.FECHA));
                str.Append("</td>");

                str.Append("<td>");
                str.Append(ipc.fechaHora.ToString(Constants.Formatos.HORA));
                str.Append("</td>");

                foreach (IConsignaExportable consigna in plantilla.consignas)
                {
                    // Buscar la instancia que corresponde con ps
                    IInstanciaPlantillaSimpleExportable ip = ipc.getInstanciaByConsigna(consigna);
                    str.Append("<td>");
                    str.Append(ip.convertToData(" - "));
                    str.Append("</td>");
                }
                str.Append("</tr>");
            }
            str.Append("</table>");
            return str.ToString();
        }

        
    }
}