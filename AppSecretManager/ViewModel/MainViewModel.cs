using System;
using System.Collections.Generic;
using System.Linq;
using AppSecretManager.Core.Extentions.Enums;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Util;
using AppSecretManager.Core.Util.Validators;
using GalaSoft.MvvmLight;
using AppSecretManager.Model;
using AppSecretManager.Validators;
using AppSecretManager.Views;
using FluentValidation.Results;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;

namespace AppSecretManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        

        

      

        #region Properties
        private readonly IDataService _dataService;
        private IList<IApiSecret> apiSecrets = new List<IApiSecret>();

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IList<IApiSecret> ApiSecrets
        {
            get
            {
                return apiSecrets;
            }
            set
            {
                Set(ref apiSecrets, value);
            }
        }


        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
           
            UpdateApiSecretList();

            CreateAPICommand = new RelayCommand(
                ExecuteCreatNewApiCommand
              );
            EditAPICommand = new RelayCommand<IApiSecret>(
                ExecuteEditAPICommand
            );

        }

        #region Commands
        public RelayCommand CreateAPICommand
        {
            get;
            private set;
        }
        private async void ExecuteCreatNewApiCommand()
        {
            var resultat = await DialogHandler.Instance.ShowDialog("RootDialog", new CreateAPISecretView() { DataContext = new CreateApiSecretViewModel(WindowOperations.Create){ApiSecret = new ApiSecret()} });
            //ResetCreateApiSecretViewModel();

            //checks if dialog vas closed, returns if it was
            if (resultat == null) return;

            InsertApiSecret(resultat as IApiSecret);
            UpdateApiSecretList();
        }

        public RelayCommand<IApiSecret> EditAPICommand
        {
            get;
            private set;
        }
        private async void ExecuteEditAPICommand(IApiSecret apiSecret)
        {
            if (apiSecret == null)
            {
                return;
            }
            var apiSecrettoEdit = apiSecret;
            var resultat = await DialogHandler.Instance.ShowDialog("RootDialog", new CreateAPISecretView() { DataContext = new CreateApiSecretViewModel(WindowOperations.Edit){ ApiSecret = apiSecrettoEdit} });
            

            //checks if dialog vas closed, returns if it was
            if (resultat == null) return;

            UpdateApiSecret(resultat as IApiSecret);
            
        }


        #endregion


        #region Functions

        //private void ResetCreateApiSecretViewModel()
        //{
        //    CreateApiSecretViewModel = new CreateApiSecretViewModel();
        //}

        private void InsertApiSecret(IApiSecret result)
        {
          
            _dataService.InsertApiSecret((success) =>
            {
                if (success)
                {
                    Console.WriteLine("Inserted Appsecret");
                    UpdateApiSecretList();
                    return;
                }
                Console.WriteLine("Failed Inserting Appsecret");
            }, result);
            
        }

        private void UpdateApiSecret(IApiSecret result)
        {

            _dataService.UpdateApiSecret((success) =>
            {
                if (success)
                {
                    Console.WriteLine("Updated Appsecret");
                    UpdateApiSecretList();
                    return;
                }
                Console.WriteLine("Failed Inserting Appsecret");
            }, result);

        }


        private void UpdateApiSecretList()
        {
            _dataService.GetAllSecrets(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    ApiSecrets = item;
                });

        }
#endregion


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}