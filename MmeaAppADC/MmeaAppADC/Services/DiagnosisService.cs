using MmeaAppADC.Interfaces;
using MmeaAppADC.Models;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmeaAppADC.Services
{
    public class DiagnosisService
    {
        public HttpClient client { get; set; }
        public DiagnosisService()
        {
            client = new HttpClient();
        }

        public async Task<ClassificationResult> GetImageClassification(Stream imageStream)
        {

            var result = await DependencyService.Resolve<IPlatformPredictionService>().Classify(imageStream);
            return result;

        }
    }
}
