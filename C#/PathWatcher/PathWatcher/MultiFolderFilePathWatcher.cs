using Example.FileHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example.PathWatcher.PathWatcher
{
    public class MultiFolderFilePathWatcher : IPathWatcher
    {
        private readonly IEnumerable<string> _folders;
        private readonly string _destinationFolder;
        private readonly CancellationToken _stoppingToken;
        private readonly ILogger<IPathWatcher> _logger;
        private readonly IFileHelpers _fileHelpers;
        private readonly IConfigurationRoot _configuration;

        public MultiFolderFilePathWatcher(IEnumerable<string> folders,
                                          string destinationFolder,
                                          CancellationToken stoppingToken,
                                          ILogger<IPathWatcher> logger,
                                          IFileHelpers fileHelpers,
                                          IConfigurationRoot configuration)
        {
            _folders = folders;
            _stoppingToken = stoppingToken;
            _destinationFolder = destinationFolder;
            _stoppingToken = stoppingToken;
            _logger = logger;
            _fileHelpers = fileHelpers;
            _configuration = configuration;
        }

        public override async Task Start()
        {
            await CustomParallel.ParallelForEachAsync(
                _folders,
                async (folder) => await StartWatchFolder(folder),
                _folders.ToArray().Length);
        }

        private async Task StartWatchFolder(string folder)
        {
            // para utilizar FileSystemWatcher
            var watcher = FromFolder(folder,
                                     _destinationFolder,
                                     _stoppingToken,
                                     _logger,
                                     _fileHelpers);

            // para recorrer las carpetas cada cierto tiempo
            //var watcher = FromFolder(folder,
            //                         _destinationFolder,
            //                         _stoppingToken,
            //                         _logger,
            //                         _fileHelpers,
            //                         _configuration);


            // registrar el método a llamar al obtener un evento
            watcher.OnNewFile += RiseNewFile;
            await watcher.Start();
        }
    }
}
