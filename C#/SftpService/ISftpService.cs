using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Example.Service
{
    public interface ISftpService
    {
        Task UploadCSV(IEnumerable<OptumNetworkDataModel> data);
    }
}
