namespace TrivialEuropeo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void PageCapitalCountry(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("capitalpais");
        }
        private async void PageCountryCapital(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("paiscapital");
        }
    }
}
