using Example.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Example.Services.Export.Strategies
{
    public class XMLExporterStrategy : IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string GetExtension()
        {
            return @"xml";
        }

        public string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement listado = doc.CreateElement(string.Empty, "Listado", string.Empty);
            doc.AppendChild(listado);
            // Recorrer todas las plantillas
            foreach (IPlantillaCompuestaExportable pc in plantillas)
            {
                XmlElement item = doc.CreateElement(string.Empty, "Item", string.Empty);
                // Código
                XmlElement elemento = doc.CreateElement(string.Empty, "Codigo", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.idTemporal));
                item.AppendChild(elemento);
                // Latitud
                elemento = doc.CreateElement(string.Empty, "Latitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.latitud + ""));
                item.AppendChild(elemento);
                // Longitud
                elemento = doc.CreateElement(string.Empty, "Longitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.longitud + ""));
                item.AppendChild(elemento);
                // Accuracy
                elemento = doc.CreateElement(string.Empty, "Accuracy", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.accuracy + ""));
                item.AppendChild(elemento);
                // Altitud
                elemento = doc.CreateElement(string.Empty, "Altitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.altitud + ""));
                item.AppendChild(elemento);
                // AltitudAccuracy
                elemento = doc.CreateElement(string.Empty, "AltitudAccuracy", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.altitudAccuracy + ""));
                item.AppendChild(elemento);
                // Fecha
                elemento = doc.CreateElement(string.Empty, "Fecha", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.fechaHora.ToString(Constants.Formatos.FECHA)));
                item.AppendChild(elemento);
                // Hora
                elemento = doc.CreateElement(string.Empty, "Hora", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.fechaHora.ToString(Constants.Formatos.HORA)));
                item.AppendChild(elemento);
                // Nombre
                elemento = doc.CreateElement(string.Empty, "Nombre", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.descripcion));
                item.AppendChild(elemento);
                // Descripción
                elemento = doc.CreateElement(string.Empty, "Descripción", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(pc.criterioDeAgrupacion));
                item.AppendChild(elemento);
                
                listado.AppendChild(item);
            }
            return doc.OuterXml;
        }

        public string Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}, {2}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias));
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement listado = doc.CreateElement(string.Empty, "Listado", string.Empty);
            doc.AppendChild(listado);

            // Carga de los datos
            foreach (IInstanciaPlantillaCompuestaExportable ipc in instancias)
            {
                XmlElement item = doc.CreateElement(string.Empty, "Item", string.Empty);
                // Código
                XmlElement elemento = doc.CreateElement(string.Empty, "Codigo", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.idTemporal));
                item.AppendChild(elemento);
                // Latitud
                elemento = doc.CreateElement(string.Empty, "Latitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.latitud + ""));
                item.AppendChild(elemento);
                // Longitud
                elemento = doc.CreateElement(string.Empty, "Longitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.longitud + ""));
                item.AppendChild(elemento);
                // Accuracy
                elemento = doc.CreateElement(string.Empty, "Accuracy", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.accuracy + ""));
                item.AppendChild(elemento);
                // Altitud
                elemento = doc.CreateElement(string.Empty, "Altitud", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.altitud + ""));
                item.AppendChild(elemento);
                // AltitudAccuracy
                elemento = doc.CreateElement(string.Empty, "AltitudAccuracy", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.altitudAccuracy + ""));
                item.AppendChild(elemento);
                // Fecha
                elemento = doc.CreateElement(string.Empty, "Fecha", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.fechaHora.ToString(Constants.Formatos.FECHA)));
                item.AppendChild(elemento);
                // Hora
                elemento = doc.CreateElement(string.Empty, "Hora", string.Empty);
                elemento.AppendChild(doc.CreateTextNode(ipc.fechaHora.ToString(Constants.Formatos.HORA)));
                item.AppendChild(elemento);
                // Resto
                foreach (IConsignaExportable consigna in plantilla.consignas)
                {
                    // Buscar la instancia que corresponde con ps
                    IInstanciaPlantillaSimpleExportable ip = ipc.getInstanciaByConsigna(consigna);

                    elemento = doc.CreateElement(string.Empty, consigna.descripcion.Replace(' ', '_'), string.Empty);
                    if (ip == null)
                        elemento.AppendChild(doc.CreateTextNode(""));
                    else
                        elemento.AppendChild(doc.CreateTextNode(ip.convertToData(" - ")));
                    item.AppendChild(elemento);
                }

                listado.AppendChild(item);
            }
            return doc.OuterXml;
        }

    }
}