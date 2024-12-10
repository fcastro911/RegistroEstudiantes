namespace RegistroEstudiantes.AppMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Vistas.ListarEstudiantes();
        }
    }
}
