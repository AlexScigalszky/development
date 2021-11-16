# Best Practices for RepositoryPatter
This is an example of best practices of repository patter using Entity Framework

# PDFCreator iTextSharp .Net Framework
There are a interface and implementation of PDF Filler using `using iText.Forms` and `using iText.Kernel.Pdf`

# PDFCreator iTextSharp .Net Core 3.1
Same of PDFCreator iTextSharp .Net Framework but using this library `https://github.com/VahidN/iTextSharp.LGPLv2.Core`

# Log Example
This is an example for a simple log. I used when is requiered delete important data (by user decision) and i want save with user make each thing. I use `AutoMapper` to manage DTOs
This was improved with LogService folder

# Iterators
There is a Interface of IQueryable using Entity Framework with a loading of a few items from database. I used when a need foreach a big collection and there are low memory

# LambdaUtilsMethods
A class herper for AND and OR expression dinamicly

# Export
This is a set of strategies to export data to an exclusive format

# Logger
This is a class Helper (DEPRECATED) for Log data avoiding touch each class. I recommend use the Log Example.

# Mail
This is a Class Helper fot send mails using  `using System.Net.Mail`, `using System.Net.Security` and `using System.Security.Cryptography.X509Certificates`
I used on .net Framework maybe it need a .net core convertion

# Serializer
A Helper class for serialize objects in a simple way using `Newtonsoft.Json` 

# HeaderFilter
This is a class to add filter on `OpenApi`

# Base32
This is a clase for conver objects from and to Base32 string

# Base64
This is a clase for conver objects from and to Base64 string

# PDFKeyValuesHelper
This is an interface and implementation of pagination for creation PDFs using dinamic forms in PDFs

# RandomGenerator
This is a class to generate a new random password with the method `NewRandomPassword` 

# StringTransformer
This is a set of method to helpme to manage strings

# SftpService
This is a services to upload a file using a SFPT protocol

# CsvHelper
This is a class to create a CSV file (in Export folder there are a others helpers as CsvHelper but using a strategy patter)

# HttpHelper
A class with some methods to help me to get data from HTTP request/responses

# HttpClient
A class to calls endpoint as http client

# ZipHelper
Helper to make zip file with a collection of files inside it

# CurrencyHelper
A class to translate a number of currency to string text (like a Thousand). TODO: Need a Unit test

# FileHelpers
File helpers to manage files in file system

# Messages
Classes to manage RabbitMQ queues (sender and recieber)

# PathWatcher
Files to watch filesystem when files was added into specific folders

# Redis
An Interface and implementation of RedisWrapper with a serie of method to put and get values and an alternative storage (using a parameter `Func`)