using CBV_SB_Shared.FileHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CBV_SB_Shared.PathWatcher.PathWatcher
{
    public class FilePathWatcher : IPathWatcher
    {
        private readonly string _folder;
        private readonly string _destinationFolder;
        private readonly CancellationToken _cancellationToken;
        private readonly ILogger<IPathWatcher> _logger;
        private readonly IFileHelpers _fileHelpers;

        public FilePathWatcher(string folder,
                               string destinationFolder,
                               CancellationToken cancellationToken,
                               ILogger<IPathWatcher> logger,
                               IFileHelpers fileHelpers)
        {
            _folder = folder;
            _destinationFolder = destinationFolder;
            _cancellationToken = cancellationToken;
            _logger = logger;
            _fileHelpers = fileHelpers;
        }

        public override async Task Start()
        {
            Console.WriteLine($"Comenzando la escucha de nuevos archivos en la carpeta { _folder }");
            _logger.LogInformation($"Comenzando la escucha de nuevos archivos en la carpeta { _folder }");

            try
            {
                using var watcher = new FileSystemWatcher
                {
                    Path = _folder
                };

                watcher.Created += async (object sender, FileSystemEventArgs e) => await EmitValue(sender, e);

                watcher.EnableRaisingEvents = true;

                //para frenar el proceso y que no se detenga en la primera ejecucion
                var tcs = new TaskCompletionSource<bool>();
                _cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
                await tcs.Task;
            }
            catch (Exception e1)
            {
                Console.WriteLine($"Ocurrio un error durante ejecución del FilePathWatcher: {e1.Message} { e1.InnerException.Message }");
                _logger.LogError($"Ocurrio un error durante ejecución del FilePathWatcher: {e1.Message} { e1.InnerException.Message }");
            }
        }

        private async Task EmitValue(object sender, FileSystemEventArgs e)
        {
            //esto es para evitar una exception mientras se copia un archivo a ese directorio
            await Task.Delay(1000);

            Console.WriteLine($"Emitiendo archivo: { e.Name }");
            _logger.LogInformation($"Emitiendo archivo: { e.Name }");

            // movemos el archivo a la carpeta destino
            try
            {
                var source = Path.Combine(_folder, e.Name);
                var destination = Path.Combine(_destinationFolder, "procesado_" + e.Name);
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
