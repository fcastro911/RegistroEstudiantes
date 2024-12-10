using Firebase.Database;

namespace RegistroEstudiantes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            RegistrarCursos();
            return builder.Build();
        }

        private static async void RegistrarCursos()
        {
            FirebaseClient client = new FirebaseClient("https://estudiantes-4a646-default-rtdb.firebaseio.com/");

            var cursosExistentes = await client.Child("Cursos").OnceAsync<string>();
            if (cursosExistentes.Count == 0)
            {
                string[] cursos = {
                    "1ro Básico", "2do Básico", "3ro Básico", "4to Básico",
                    "5to Básico", "6to Básico", "7mo Básico", "8vo Básico",
                    "1ro Medio", "2do Medio", "3ro Medio", "4to Medio"
                };
                foreach (var curso in cursos)
                {
                    await client.Child("Cursos").PostAsync(curso);
                }
            }
        }
    }
}
