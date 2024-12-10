using Firebase.Database;
using Firebase.Database.Query;
using RegistroEstudiantes.Modelos.Modelos;

namespace RegistroEstudiantes.AppMovil.Vistas;

public partial class CrearEstudiante : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://estudiantes-4a646-default-rtdb.firebaseio.com/");

    public List<Curso> Cursos { get; set; }
    public CrearEstudiante()
    {
        InitializeComponent();
        ListarCursos();
        BindingContext = this;
    }

    private void ListarCursos()
    {
        var cursos = client.Child("Cursos").OnceAsync<Curso>();
        Cursos = cursos.Result.Select(x=>x.Object).ToList();
    }

    private async void guardarButton_Clicked(object sender, EventArgs e)
    {
        Curso curso = cursoPicker.SelectedItem as Curso;

        var estudiante = new Estudiante
        {
            Nombre = NombreEntry.Text,
            Apellido = ApellidoEntry.Text,
            CorreoElectronico = CorreoElectronicoEntry.Text,
            Edad = int.Parse(EdadEntry.Text),
            Curso = curso
        };

        try
        {
            await client.Child("Estudiantes").PostAsync(estudiante);
            await DisplayAlert("�xito", $"El Estudiante {estudiante.Nombre} {estudiante.Apellido} fue guardado correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message,"OK");
        }
           
    }
}