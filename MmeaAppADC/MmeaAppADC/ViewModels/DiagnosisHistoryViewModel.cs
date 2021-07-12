using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public DiagnosisHistoryViewModel()
        {
            _dbService = new DBservice();
            Diagnoses = new ObservableCollection<UserDiagnosis>();
            GetDiagnoses();
        }
        private async void GetDiagnoses()
        {
            List<UserDiagnosis> list = await _dbService.GetUserDiagnosis();
            foreach (var user in list)
            {
                Diagnoses.Add(user);
            }
        }
    }
}
