namespace TrivialEuropeo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("capitalpais", typeof(CapitalPais));
            Routing.RegisterRoute("paiscapital", typeof(PaisCapital));
        }
    }
}
