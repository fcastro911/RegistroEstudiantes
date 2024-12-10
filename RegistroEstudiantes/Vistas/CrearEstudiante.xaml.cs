using Firebase.Database;
using Firebase.Database.Query;
using RegistroEstudiantes.Modelos;

namespace RegistroEstudiantes.Vistas
{
    public partial class CrearEstudiante : ContentPage
    {
        FirebaseClient client = new FirebaseClient("https://estudiantes-4a646-default-rtdb.firebaseio.com/");

        public CrearEstudiante()
        {
            InitializeComponent();
            CargarCursos();
        }

        private async void CargarCursos()
        {
            try
            {
                // Obtener cursos como lista de strings desde Firebase
                var cursos = await client.Child("Cursos").OnceAsync<string>();
                cursoPicker.ItemsSource = cursos.Select(c => c.Object).ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los cursos: {ex.Message}", "OK");
            }
        }

        private async void guardarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validar entrada de datos
                if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
                    string.IsNullOrWhiteSpace(correoEntry.Text) ||
                    string.IsNullOrWhiteSpace(edadEntry.Text) ||
                    cursoPicker.SelectedItem == null)
                {
                    await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                    return;
                }

                // Crear un nuevo estudiante
                var estudiante = new Estudiante
                {
                    NombreCompleto = nombreEntry.Text,
                    CorreoElectronico = correoEntry.Text,
                    Edad = int.TryParse(edadEntry.Text, out var edad) ? edad : 0,
                    Curso = cursoPicker.SelectedItem.ToString(),
                    Activo = activoSwitch.IsToggled
                };

                // Guardar estudiante en Firebase
                await client.Child("Estudiantes").PostAsync(estudiante);

