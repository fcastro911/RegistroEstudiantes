using Firebase.Database;
using RegistroEstudiantes.Modelos;

namespace RegistroEstudiantes.Vistas
{
    public partial class ListarEstudiantes : ContentPage
    {
        FirebaseClient client = new FirebaseClient("https://estudiantes-4a646-default-rtdb.firebaseio.com/");
        List<Estudiante> estudiantes = new List<Estudiante>();

        public ListarEstudiantes()
        {
            InitializeComponent();
            CargarEstudiantes();
        }

        private async void CargarEstudiantes()
        {
            try
            {
                // Cargar estudiantes desde Firebase
                var estudiantesQuery = await client.Child("Estudiantes").OnceAsync<Estudiante>();
                estudiantes = estudiantesQuery.Select(e => e.Object).ToList();

                // Mostrar solo los estudiantes activos
                estudiantesCollection.ItemsSource = estudiantes.Where(e => e.Activo).ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los estudiantes: {ex.Message}", "OK");
            }
        }

        private void BuscarBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filtro = e.NewTextValue?.ToLower() ?? string.Empty;

                // Filtrar estudiantes por nombre
                estudiantesCollection.ItemsSource = estudiantes
                    .Where(e => e.NombreCompleto.ToLower().Contains(filtro))
                    .ToList();
            }
            catch
            {
                estudiantesCollection.ItemsSource = estudiantes;
            }
        }

        private async void NuevoEstudianteButton_Clicked(object sender, EventArgs e)
        {
            // Navegar a la página de creación de estudiantes
            await Navigation.PushAsync(new CrearEstudiante());
        }
    }
}
