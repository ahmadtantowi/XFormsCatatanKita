using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFormsCatatanKita
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatatansPage : ContentPage
	{
        private CatatansDataAccess dataAccess;

		public CatatansPage ()
		{
			InitializeComponent ();

            // An instance of the CatatansDataAccessClass
            // that is used for data-binding and data access
            this.dataAccess = new CatatansDataAccess();
        }

        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CatatansDataAccess
            // is the data binding source
            this.BindingContext = this.dataAccess;
        }

        // Save any pending changes
        private void OnSaveClick(object sender, EventArgs e)
        {
            this.dataAccess.SaveAllCatatans();
        }

        // Add a new catatan to the Catatans collection
        private void OnAddClick(object sender, EventArgs e)
        {
            this.dataAccess.AddNewCatatan();
        }

        // Remove the current catatan
        // If it exist in the database, it will be removed
        // from there too
        private void OnRemoveClick(object sender, EventArgs e)
        {
            var currentCatatan = this.CatatansView.SelectedItem as Catatan;
            if (currentCatatan != null)
            {
                this.dataAccess.DeleteCatatan(currentCatatan);
            }
        }

        // Remove all catatans
        // Use a DisplayAlert object to ask the user's confirmation
        private async void OnRemoveAllClick(object sender, EventArgs e)
        {
            if (this.dataAccess.Catatans.Any())
            {
                var result =
                  await DisplayAlert("Konfirmasi",
                  "Apa kamu yakin akan menghapus semua catatan?",
                  "OK", "Cancel");
                if (result == true)
                {
                    this.dataAccess.DeleteAllCatatans();
                    this.BindingContext = this.dataAccess;
                }
            }
        }
    }
}