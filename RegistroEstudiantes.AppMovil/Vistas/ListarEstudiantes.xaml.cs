namespace RegistroEstudiantes.AppMovil.Vistas;

public partial class ListarEstudiantes : ContentPage
{
	public ListarEstudiantes()
	{
		InitializeComponent();
	}

    private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private async void NuevoEstudianteBoton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearEstudiante());
    }
}