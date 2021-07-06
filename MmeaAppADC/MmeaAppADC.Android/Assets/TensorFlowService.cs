using AzureCustomVisionDemo.Droid;
using MmeaAppADC.Interfaces;
using MmeaAppADC.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xam.Plugins.OnDeviceCustomVision;
using Xamarin.Forms;

[assembly: Dependency(typeof(TensorFlowService))]
namespace AzureCustomVisionDemo.Droid
{
    public class TensorFlowService : IPlatformPredictionService
    {
        public async Task<ClassificationResult> Classify(Stream imageStream)
        {
            // You can also do this from your shared Forms code! 🤯
            // See MainPage.xaml.cs, line 79
            // You can also implement the native Android TensorFlow code here
            var tags = await CrossImageClassifier.Current.ClassifyImage(imageStream);
            var bestResult = tags.OrderByDescending(t => t.Probability).First();

            return new ClassificationResult(bestResult.Tag, bestResult.Probability);
        }
    }
}