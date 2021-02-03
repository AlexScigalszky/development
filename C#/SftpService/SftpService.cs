using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Interfaces;
using Infrastructure.Services;
using Configuration;
using Renci.SshNet;

namespace Example.Service
{
    public class SftpService<T> : ISftpService
    {
        private readonly ConnectionSftp _config;
        private readonly ICsvHelperService _csvHelperService;
        private readonly ILogService _logService;

        private const string NAME_PREFIX = "FtpFile_";
        private const string NAME_CSV = ".csv";

        public SftpService(ConnectionSftp sftpConfig, 
            ICsvHelperService csvHelperService, 
            ILogService logService)
        {
            _config = sftpConfig ?? throw new ArgumentNullException(nameof(sftpConfig));
            _csvHelperService = csvHelperService ?? throw new ArgumentNullException(nameof(csvHelperService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task UploadCSV(IEnumerable<T> data)
        {
            await LogInfo($"{DateTime.Now} - Starting upload...", "Starting upload ...");
            
            var now = DateTime.Now;
            var name = string.Format("{0}{1}{2}{3}{4}", 
                NAME_PREFIX, 
                now.ToString("dd"), 
                now.ToString("MM"), 
                now.Year, 
                NAME_CSV);

            var port = _config.Port == 0 ? 22 : _config.Port;
            var connectionInfo = new PasswordConnectionInfo(_config.Server, port, _config.UserName, _config.Password)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };

            await LogInfo($"{DateTime.Now} - Creating new client ...", "Creating new client ...");
            using var client = new SftpClient(connectionInfo)
            {
                OperationTimeout = TimeSpan.FromMinutes(3)
            };
            await LogInfo($"{DateTime.Now} - Client created.", "Client created.");
            try
            {
                await LogInfo($"{DateTime.Now} - Connecting client ...", "Connecting client ...");
                client.Connect();
                client.BufferSize = 4 * 1024; //bypass Payload error large files
                await LogInfo($"{DateTime.Now} - Client connected.", "Client connected.");

                await LogInfo($"{DateTime.Now} - Creating CSVFile ...", "Creating CSVFile ...");
                var resStream = await _csvHelperService.CreateCSVStreamAsync(data);
                resStream.Position = 0;

                await LogInfo($"{DateTime.Now} - Uploading CSVFile: {name} ...", "Uploading CSVFile: {name} ...");
                client.UploadFile(resStream, _config.Directory + name, true);
                await LogInfo($"{DateTime.Now} - Finished uploading file '{name}' to '{_config.Directory}'", "Finished uploading file '{name}' to '{_config.Directory}'");
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine($"{DateTime.Now} - Failed in uploading file '{name}' to '{_config.Directory}'");
                Console.Out.WriteLine(exception.Message);
                await _logService.LogError<SftpService>($"{DateTime.Now} - Failed in uploading file '{name}' to '{_config.Directory}'", exception.Message);
            }
            finally
            {
                client.Disconnect();
            }
        }

        private async Task LogInfo(string message, string detail)
        {
            Console.Out.WriteLine(message);
            await _logService.LogInformation<SftpService>(message, detail);
        }
    }
}
