using System.Threading.Tasks;
using PharmaCoreApi.Models;

namespace PharmaCoreApi.Models
{
    public interface IFileRepository
    {
        Task<string> WriteTextAsync(string filePath, DrugDetails drugDetails);
    }
}
