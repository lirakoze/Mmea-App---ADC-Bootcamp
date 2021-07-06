using MmeaAppADC.Models;
using System.IO;
using System.Threading.Tasks;

namespace MmeaAppADC.Interfaces
{
    public interface IPlatformPredictionService
    {
        Task<ClassificationResult> Classify(Stream imageStream);
    }
}
