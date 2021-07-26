using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class DiagnosisHistoryViewModel : BaseViewModel
    {
        private DBservice _dbService;
        private ObservableCollection<UserDiagnosis> diagnoses;
        public ObservableCollection<UserDiagnosis> Diagnoses
        {
            get { return diagnoses; }
            set { diagnoses = value; OnPropertyChanged(); }
        }
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }

        public Command RefreshCommand { get; set; }
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }

        public DiagnosisHistoryViewModel()
        {
            _dbService = new DBservice();
            Diagnoses = new ObservableCollection<UserDiagnosis>();
            RefreshCommand = new Command(async () => await Refresh());
            IsRefreshing = false;
            IsVisible = false;
            _ = GetDiagnoses();
        }

        private async Task Refresh()
        {
            Diagnoses.Clear();
            await GetDiagnoses();
            IsRefreshing = false;
        }

        private async Task GetDiagnoses()
        {
            List<UserDiagnosis> list = await _dbService.GetUserDiagnosis();
            foreach (var user in list)
            {
                Diagnoses.Add(user);
            }
            if (list.Count == 0)
                IsVisible = true;
        }
    }
}
