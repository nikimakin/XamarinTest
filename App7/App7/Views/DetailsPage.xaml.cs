using App7.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// подключаем DataContractJsonSerializer 
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace App7.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(XmlOfferDetails item)
        {
            InitializeComponent();
            // создаем DataContractJsonSerializer 
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(XmlOfferDetails));

            // создаем поток (json файл) 
            MemoryStream stream = new MemoryStream();
          
                // сериализация (сохранение объекта в поток) 
                formatter.WriteObject(stream, item);

            selected.Text = Encoding.Default.GetString((stream.ToArray()));
            GridDetails.BindingContext = item;
        }
    }
}
