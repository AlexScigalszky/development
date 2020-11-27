using Example.Models;
using NLog;
using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;

namespace Example.Services.Export.Strategies
{
    public class KMLExporterStrategy : IExportStrategy
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string GetExtension()
        {
            logger.Info("getExtension");
            return @"kml";
        }

        public string Export(IEnumerable<IPlantillaCompuestaExportable> plantillas)
        {
            logger.Info("export {0}", Utilities.Serializer.Serialize2JSON(plantillas));
            // Crear el estilo para el ícono
            var style = new Style();
            style.Id = "plantillaSimpleIcon";
            style.Icon = new IconStyle();
            //style.Icon.Color = new Color32(255, 0, 255, 0);
            //style.Icon.ColorMode = ColorMode.Normal;
            style.Icon.Icon = new IconStyle.IconLink(new Uri("http://www.tatuel.com.ar/assets/img/plantillas/plantilla-simple.png"));
            style.Icon.Scale = 1.1;
            // Conjunto de localizaciones
            Folder carpeta = new Folder();
            carpeta.Name = "Localizaciones de referencia";
            carpeta.AddStyle(style);
            // Recorrer cada instancia de plantilla simple y cargar ese punto
            Point point;
            foreach (IPlantillaCompuestaExportable pc in plantillas)
            {

                point = new Point();
                point.AltitudeMode = AltitudeMode.Absolute;
                point.Id = "Tatuel";
                point.TargetId = pc.idTemporal;
                point.Coordinate = new Vector(pc.latitud, pc.longitud, pc.altitud);
                Placemark marca = new Placemark();
                marca.Name = "" + pc.descripcion;
                marca.Geometry = point;
                // descripcion 
                marca.Description = new Description();
                marca.Description.Text = pc.descripcion;
                marca.ExtendedData = new ExtendedData();
                marca.ExtendedData.AddData(new Data()
                {
                    DisplayName = pc.criterioDeAgrupacion,
                    Name = "Nombre",
                    Value = pc.descripcion
                });
                marca.Time = new Timestamp()
                {
                    When = pc.fechaHora
                };
                marca.StyleUrl = new Uri("#plantillaSimpleIcon", UriKind.Relative);
                // agregarlo a la carpeta
                carpeta.AddChild(marca);
            }
            // Crear el archivo KML con esa carpeta
            Kml kml = new Kml();
            kml.Feature = carpeta;
            // Serializar y devolver
            Serializer serializerKml = new Serializer();
            serializerKml.Serialize(kml);
            return serializerKml.Xml;
        }

        public string Export(IPlantillaCompuestaExportable plantilla, IEnumerable<IInstanciaPlantillaCompuestaExportable> instancias)
        {
            logger.Info("export {0}, {1}", Utilities.Serializer.Serialize2JSON(plantilla), Utilities.Serializer.Serialize2JSON(instancias));
            // Crear el estilo para el ícono
            var style = new Style();
            style.Id = "plantillaSimpleIcon";
            style.Icon = new IconStyle();
            //style.Icon.Color = new Color32(255, 0, 255, 0);
            //style.Icon.ColorMode = ColorMode.Normal;
            style.Icon.Icon = new IconStyle.IconLink(new Uri("http://www.tatuel.com.ar/assets/img/plantillas/plantilla-simple.png"));
            style.Icon.Scale = 1.1;
            // Conjunto de localizaciones
            Folder carpeta = new Folder();
            carpeta.Name = "Localizaciones de carga de datos";
            carpeta.AddStyle(style);
            // Recorrer cada instancia de plantilla simple y cargar ese punto
            Point point;
            foreach (Models.IInstanciaPlantillaCompuestaExportable ipc in instancias)
            {
                //if (ipc.posicion != null)
                //{
                point = new Point();
                point.AltitudeMode = AltitudeMode.Absolute;
                point.Id = "Tatuel";
                //LatitudLongitud latlon = (LatitudLongitud)ipc.posicion;
                point.Coordinate = new Vector(ipc.latitud, ipc.longitud, ipc.altitud);
                Placemark marca = new Placemark();
                marca.Name = "" + ipc.descripcion;
                marca.Geometry = point;
                // descripcion 
                marca.Description = new Description();
                marca.Description.Text = "Punto de carga de datos ";
                marca.ExtendedData = new ExtendedData();
                marca.ExtendedData.AddData(new Data()
                {
                    DisplayName = "Datos serializados en formato JSON",
                    Name = "Datos serializados en formato JSON",
                    Value = Utilities.Serializer.Serialize2JSON(new
                    {
                        accuracy = ipc.accuracy,
                        altitud = ipc.altitud,
                        altitudAccuracy = ipc.altitudAccuracy,
                        descripcion = ipc.descripcion,
                        fechaHora = ipc.fechaHora,
                        idTemporal = ipc.idTemporal,
                        latitud = ipc.latitud,
                        longitud = ipc.longitud
                    })
                });
                marca.Time = new Timestamp()
                {
                    When = ipc.fechaHora
                };
                marca.StyleUrl = new Uri("#plantillaSimpleIcon", UriKind.Relative);
                // agregarlo a la carpeta
                carpeta.AddChild(marca);
                //}
            }
            // Crear el archivo KML con esa carpeta
            Kml kml = new Kml();
            kml.Feature = carpeta;
            // Serializar y devolver
            Serializer serializerKml = new Serializer();
            serializerKml.Serialize(kml);
            return serializerKml.Xml;
        }
    }
}