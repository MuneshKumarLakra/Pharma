using PharmaCoreApi.Helper;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCoreApi.Models
{
    public class FileRepository : IFileRepository
    {
        public async Task<string> WriteTextAsync(string filePath, DrugDetails drugDetails)
        {
            bool isAddedOrAppended = false;
            string text = drugDetails.DrugName + " " + drugDetails.DrugExpiredOn + drugDetails.LotNo + Environment.NewLine;

            string SAMPLE_KEY = "gCjK+DZ/GCYbKIGiAt1qCA==";
            string SAMPLE_IV = "47l5QsSe1POo31adQ/u7nQ==";

            var key = Encoding.UTF8.GetBytes(SAMPLE_KEY);
            var iv = Encoding.UTF8.GetBytes(SAMPLE_IV);

            Array.Resize(ref key, 128 / 8);
            Array.Resize(ref iv, 128 / 8);

            if (File.Exists(filePath) && File.ReadLines(filePath).Any(line => line.Length > 0))
                isAddedOrAppended = await StreamHelper.AppendStringToFile(filePath, text, key, iv);
            else
                isAddedOrAppended = await StreamHelper.WriteStringToFile(filePath, text, key, iv);

            return isAddedOrAppended ? "success" : "failure";
        }
    }
}
