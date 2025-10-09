using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Interface
{
    public interface IOCRService
    {
        Task<string> ExtractTextFromCedulaAsync(IFormFile imageFile);
        Task<bool> ValidateDocumentNumberAsync(IFormFile cedulaImage, string expectedDocumentNumber);
    }
}
