using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSecretManager.Core.Extentions.Enums;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Util;
using AppSecretManager.Core.Util.Validators;
using AppSecretManager.Model;
using AppSecretManager.Validators;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;

namespace AppSecretManager.ViewModel
{
    public class CreateApiSecretViewModel : ViewModelBase
    {
        #region Errors
        private bool _errors = false;
        private string error = string.Empty;

        public bool Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                RaisePropertyChanged();
            }
        }
        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged();
            }
        }
#endregion

        #region Commands

        public RelayCommand GenerateAPIKeyCommand { get; private set; }

        public RelayCommand<bool> CloseDialogCommand { get; private set; }


        private void ExecuteCloseDialogCommand(bool close)
        {
            if (!close)
            {
                DialogHandler.Instance.CloseDialog();
                return;
            }
            var getapisecret = ApiSecret;
            //validates the result 
            var result = ValidationHandler.ValidateIApiSecret(getapisecret);

            if (!result.Item1)
            {
                Errors = !result.Item1;
                Error = result.Item2;
                return;
            }
            DialogHandler.Instance.CloseDialog(getapisecret);
        }


        private void ExecuteGenerateAPIKeyCommand()
        {
            ApiSecret.Secret = SecretGenerator.RandomString(25);
        }

        #endregion

        #region Properties

        

        private List<EnumModel> _enumModels = new List<EnumModel>();
        private IApiSecret _apiSecret = new ApiSecret();
        private bool _inCreateMode;
        public string ConfirmButtonText { get; set; } = "Create";

        public IApiSecret ApiSecret
        {
            get
            {
                
                    var allowedApistrings = EnumModels.Where(x => x.Enabled)
                        .Select(x => x.Type.ToEnum<ApiType>()).ToList();
                    _apiSecret.AllowedApis = allowedApistrings;
                

                return _apiSecret;
            }
            set
            {
                SetupEnumModels(value);
                _apiSecret = value;
            }
        }

        public List<EnumModel> EnumModels
        {
            get { return _enumModels; }
            set { _enumModels = value; }
        }

        public bool InCreateMode
        {
            get => _inCreateMode;
            set
            {
                _inCreateMode = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        public CreateApiSecretViewModel(WindowOperations operation): this ()
        {
            
            switch (operation)
            {
                case WindowOperations.Create:
                    InCreateMode = true;
                    ConfirmButtonText = "Create";
                    break;
                case WindowOperations.Edit:
                    InCreateMode = false;
                    ConfirmButtonText = "Update";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }

        public CreateApiSecretViewModel()
        {
            
            
            GenerateAPIKeyCommand = new RelayCommand(
                ExecuteGenerateAPIKeyCommand
            );
            CloseDialogCommand = new RelayCommand<bool>(
                ExecuteCloseDialogCommand
            );
        }


        private void SetupEnumModels(IApiSecret secret)
        {
            foreach (string type in Enum.GetNames(typeof(ApiType)).ToList())
            {
                if (secret.AllowedApis.Contains(type.ToEnum<ApiType>()))
                {
                    EnumModels.Add(new EnumModel(type, true));
                    continue;
                }
                EnumModels.Add(new EnumModel(type, false));
            }

        }

      
    }

    public enum WindowOperations
    {
        Create,
        Edit
    }

    #region New Class - EnumModel

    public class EnumModel: ViewModelBase
    {
        private string _type;
        private bool _enabled = false;

        public EnumModel(string type, bool enabled)
        {
            Type = type;
            Enabled = enabled;
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                RaisePropertyChanged();
            }
        }
    }
    #endregion
}