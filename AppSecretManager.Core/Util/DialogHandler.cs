using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MaterialDesignThemes.Wpf;

namespace AppSecretManager.Core.Util
{
    public sealed class DialogHandler
    {
        private static readonly DialogHandler instance = new DialogHandler();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DialogHandler()
        {
        }

        private DialogHandler()
        {
        }

        public static DialogHandler Instance
        {
            get { return instance; }
        }

        public DialogSession DialogSession { get; set; }

        public async Task<object> ShowDialog(string id, object view)
        {
            void Openedhandler(object sender, DialogOpenedEventArgs eventArgs)
            {
                DialogSession = eventArgs.Session;
            }

            void Closinghandler(object sender, DialogClosingEventArgs e)
            {
                DialogSession = null;
            }

            return await DialogHost.Show(view, id, Openedhandler, Closinghandler);
        }

        public void CloseDialog(object parameter)
        {
            DialogSession?.Close(parameter);
        }

        public void CloseDialog()
        {
            DialogSession?.Close();
        }
    }
}