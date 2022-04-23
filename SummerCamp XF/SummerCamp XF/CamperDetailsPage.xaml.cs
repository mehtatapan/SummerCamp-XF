using SummerCamp_XF.Data;
using SummerCamp_XF.Models;
using SummerCamp_XF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SummerCamp_XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CamperDetailsPage : ContentPage
    {
        private Camper camper;
        private App thisApp;
        private List<Compound> Compounds;

        public CamperDetailsPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            Compounds = new List<Compound>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            camper = (Camper)this.BindingContext;
            if (camper.ID == 0)//Adding New
            {
                this.Title = "Add New Camper";
                //Since we do not have an ArtType yet, we want to get one ready
                Compound noCompound = new Compound { ID = 0, Name = " Select a Compound" };
                Compounds.Add(noCompound);
                btnDelete.IsEnabled = false;
            }
            else
            {
                this.Title = "Edit Camper Details";
                btnDelete.IsEnabled = true;
            }

            FillCompounds();
        }

        private void FillCompounds()
        {

            foreach (Compound d in thisApp.AllCompounds.OrderBy(d => d.Name))
            {
                Compounds.Add(d);
            }
            //Fill the Drop Down Items
            ddlCompounds.ItemsSource = Compounds;
            //Set value to current
            if (camper.CompoundID >= 0)
            {
                ddlCompounds.SelectedItem = thisApp.AllCompounds.FirstOrDefault(d => d.ID == camper.CompoundID);
            }
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            try
            {
                //If a new record, artwork.ArtType will still be null so try to get it
                camper.Compound = (Compound)ddlCompounds.SelectedItem;
                //If nothing is selected then we still want 0 for the foreign key
                camper.CompoundID = (camper.Compound?.ID).GetValueOrDefault();

                if (camper.CompoundID > 0)
                {
                    CamperRepository r = new CamperRepository();
                    if (camper.ID == 0)
                    {
                        await r.AddCamper(camper);
                    }
                    else
                    {
                        await r.UpdateCamper(camper);
                    }
                    thisApp.needCamperRefresh = true;
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Compound Not Selected:", "You must set the Compound for the Camper.", "Ok");
                }

            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                thisApp.needCamperRefresh = true;
                await DisplayAlert("Problem Saving the Camper:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Could not complete operation.", "Ok");
                }
            }
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + camper.FullName + "?", "Yes", "No");
            if (answer == true)
            {
                try
                {
                    CamperRepository er = new CamperRepository();
                    await er.DeleteCamper(camper);
                    thisApp.needCamperRefresh = true;
                    await Navigation.PopAsync();
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    await DisplayAlert("Problem Deleting the Camper:", sb.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {
                        await DisplayAlert("Error", "No connection with the server.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Could not complete operation.", "Ok");
                    }
                }
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}