using Example.FileHelpers;
using Example.PathWatcher.PathWatcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.PathWatcher
{
    public abstract class IPathWatcher
    {
        public delegate void NewPathHandler(IPathWatcher sender, string path);
        public event NewPathHandler OnNewFile;

        public static IPathWatcher FromFolder(string folder,
                                              string destinationFolder,
                                              CancellationToken cancellationToken,
                                              ILogger<IPathWatcher> logger,
                                              IFileHelpers fileHelpers)
        {
            return new FilePathWatcher(folder,
                                       destinationFolder,
                                       cancellationToken,
                                       logger,
                                       fileHelpers);
        }

        public static IPathWatcher FromFolder(string folder,
                                              string destinationFolder,
                                              CancellationToken cancellationToken,
                                              ILogger<IPathWatcher> logger,
                                              IFileHelpers fileHelpers,
                                              IConfigurationRoot configuration)
        {
            return new PoolingFilesWatcher.PoolingFilesWatcher(folder,
                                                               destinationFolder,
                                                               cancellationToken,
                                                               logger,
                                                               fileHelpers,
                                                               configuration);
        }

        public static IPathWatcher FromFolder(IEnumerable<string> folders,
                                              string destinationFolder,
                                              CancellationToken cancellationToken,
                                              ILogger<IPathWatcher> logger,
                                              IFileHelpers fileHelpers,
                                              IConfigurationRoot configuration)
        {
            return new MultiFolderFilePathWatcher(folders,
                                                  destinationFolder,
                                                  cancellationToken,
                                                  logger,
                                                  fileHelpers,
                                                  configuration);
        }

        protected void RiseNewFile(IPathWatcher sender, string path)
        {
            OnNewFile?.Invoke(sender, path);
        }

        public abstract Task Start();
    }
}
