using Firebase.Database;
using Firebase.Database.Query;
using RegistroEstudiantes.Modelos.Modelos;

namespace RegistroEstudiantes.AppMovil.Vistas;

public partial class CrearEstudiante : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://estudiantes-4a646-default-rtdb.firebaseio.com/");
    public CrearEstudiante()
    {
        InitializeComponent();
        BindingContext = this;
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

        await client.Child("Estudiantes").PostAsync(estudiante);

        await DisplayAlert("Éxito", $"El Estudiante {estudiante.Nombre} {estudiante.Apellido} fue guardado correctamente", "OK");

        await Navigation.PopAsync();
    }
}