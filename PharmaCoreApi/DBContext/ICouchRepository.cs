using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCoreApi.Models
{
    public interface ICouchRepository
    {
        Task<HttpClientResponse> PostDocumentAsync(PharmaDetails pharmaDetails);

        Task<HttpClientResponse> FindDocument(Query pharmaDetails);
        Task<HttpClientResponse> PutDocumentAsync(UpdatePharmaDetails update);
        Task<HttpClientResponse> GetDocumentAsync(string id);
        Task<HttpClientResponse> GetAllDocumentsAsync();
        Task<HttpClientResponse> GetViewAsync(QueryView key);
        Task<HttpClientResponse> DeleteDocumentAsync(string id, string rev);


    }
}
