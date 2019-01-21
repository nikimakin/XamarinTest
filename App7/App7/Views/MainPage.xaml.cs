using App7.Model;
using App7.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            listviewOffer.ItemSelected += ListviewOffer_ItemSelected;
            GetRequest();
        }
        public async void GetRequest()
        {
            //Check network status  
            if (NetworkCheck.IsInternet())
            {          
                Uri geturi = new Uri("https://yastatic.net/market-export/_/partner/help/YML.xml");
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();//Getting response  

                //Xml Parsing  
                List<XmlOfferDetails> ObjOfferList = new List<XmlOfferDetails>();
                XDocument doc = XDocument.Parse(response);
                foreach (var item in doc.Descendants("offer"))
                {
                    XmlOfferDetails ObjOfferItem = new XmlOfferDetails();
                    ObjOfferItem.Id = item.Attribute("id").Value.ToString();
                    ObjOfferItem.Type = item.Attribute("type").Value.ToString();
                    ObjOfferItem.Bid = item.Attribute("bid").Value.ToString();
                    ObjOfferItem.Available = item.Attribute("available").Value.ToString();        
                    ObjOfferList.Add(ObjOfferItem);
                }
                //Binding listview with server response  
                listviewOffer.ItemsSource = ObjOfferList;
            }
            else
            {
                await DisplayAlert("XmlParsing", "No network is available.", "Ok");
            }
            //Hide loader after server response  
            ProgressLoader.IsRunning = false;
        }
        private void ListviewOffer_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemSelectedData = e.SelectedItem as XmlOfferDetails;
            Navigation.PushAsync(new DetailsPage(itemSelectedData));
        }
    }
}
