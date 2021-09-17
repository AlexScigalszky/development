using CBV_SB_Shared.FileHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CBV_SB_Shared.PathWatcher.PoolingFilesWatcher
{
    public class PoolingFilesWatcher : IPathWatcher
    {
        private readonly string _folder;
        private readonly string _destinationFolder;
        private readonly CancellationToken _cancellationToken;
        private readonly ILogger<IPathWatcher> _logger;
        private readonly IFileHelpers _fileHelpers;
        private readonly IConfigurationRoot _configuration;

        public PoolingFilesWatcher(string folder,
                                   string destinationFolder,
                                   CancellationToken cancellationToken,
                                   ILogger<IPathWatcher> logger,
                                   IFileHelpers fileHelpers,
                                   IConfigurationRoot configuration)
        {
            _folder = folder;
            _destinationFolder = destinationFolder;
            _cancellationToken = cancellationToken;
            _logger = logger;
            _fileHelpers = fileHelpers;
            _configuration = configuration;
        }

        public override async Task Start()
        {
            Console.WriteLine($"Comenzando la escucha de nuevos archivos en la carpeta { _folder }");
            _logger.LogInformation($"Comenzando la escucha de nuevos archivos en la carpeta { _folder }");

            var delay = int.Parse(_configuration["ConfiguracionInicial:FilePollingDelay"]);
            // mientras no se haya cancelado la tarea
            while (!_cancellationToken.IsCancellationRequested)
            {
                // busco los archivos existentes en la carpeta
                var fileNames = _fileHelpers.GetFiles(_folder);

                // emitimos el valor para cada archivo 
                foreach (var filename in fileNames)
                {
                    EmitValue(filename);
                }

                // esperamos un tiempo para volver a chequear archivos nuevos
                await Task.Delay(delay);
            }
        }

        private void EmitValue(string filename)
        {
            Console.WriteLine($"Emitiendo archivo: { filename }");
            _logger.LogInformation($"Emitiendo archivo: { filename }");

            // movemos el archivo a la carpeta destino
            try
            {
                var source = Path.Combine(_folder, filename);
                var destination = Path.Combine(_destinationFolder, "procesado_" + filename);
                _fileHelpers.CopyAndDelete(source, destination);

                // disparamos el nuevo evento
                RiseNewFile(this, destination);

            }
            catch (Exception e1)
            {
                _logger.LogError($"Se genero la siguiente excepcion durante la copia de archivos: {e1.Message} { e1.InnerException.Message }");
            }
        }
    }
}
