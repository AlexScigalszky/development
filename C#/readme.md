# Best Practices for RepositoryPatter
This is an example of best practices of repository patter using Entity Framework

# PDFCreator iTextSharp .Net Framework
There are a interface and implementation of PDF Filler using `using iText.Forms` and `using iText.Kernel.Pdf`

# PDFCreator iTextSharp .Net Core 3.1
Same of PDFCreator iTextSharp .Net Framework but using this library `https://github.com/VahidN/iTextSharp.LGPLv2.Core`

# Log Example
This is an example for a simple log. I used when is requiered delete important data (by user decision) and i want save withc user make each thing. I use `AutoMapper` to manage DTOs

# Iterators
There is a Interface of IQueryable using Entity Framework with a loading of a few items from database. I used when a need foreach a big collection and there are low memory

# LambdaUtilsMethods
A class herper for AND and OR expression dinamicly

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
This is a clase for conver objects from and to Base64 string