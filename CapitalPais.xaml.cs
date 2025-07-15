namespace TrivialEuropeo;

public partial class CapitalPais : ContentPage
{
    //al añadir un signo de interrogación, estamos diciendo que se aceptan valores nulos
    private string? capitalGenerado;
    private string? capitalCorrecta;
    private Random random = new Random();
    private int respuestaCorrecta = 0;
    private int respuestaIncorrecta = 0;
    private int numPreguntas = 0;
    private Button[] botones;

    private readonly string[] capitales = {"Berlín", "Viena", "Bruselas", "Sofía", "Nicosia", "Zagreb", "Copenhague",
    "Bratislava", "Liubliana" , "Madrid", "Tallin", "Helsinki", "París", "Atenas", "Budapest", "Dublín",
    "Roma", "Riga", "Vilna", "Luxemburgo", "La Valeta", "Ámsterdam", "Varsovia", "Lisboa", "Londres",
    "Praga", "Bucarest", "Estocolmo"};
    private readonly string[] paises = {"Alemania", "Austria", "Bélgica", "Bulgaria", "Chipre", "Croacia", "Dinamarca",
    "Eslovaquia", "Eslovenia", "España", "Estonia", "Finlandia", "Francia", "Grecia", "Hungría", "Irlanda",
    "Italia", "Letonia", "Lituania", "Luxemburgo", "Malta", "Países Bajos", "Polonia", "Portugal",
    "Reino Unido", "Chéquia", "Rumanía", "Suecia"};

    public CapitalPais()
    {
        InitializeComponent();
        //iniciar arreglo de botones
        botones = new Button[] { Btn1, Btn2, Btn3, Btn4 };
        MostrarPaisesAleatorios();
    }
    private void MostrarPaisesAleatorios()
    {
        //Hasta que el usuario no elija una respuesta, no podrá pasar a la siguiente pregunta.
        Next.IsEnabled = false;

        //creamos listas para evitar que se pregunten las mismas capitales.
        List<string> capitalesDisponibles = new List<string>(capitales);
        List<string> paisesDisponibles = new List<string>(paises);

        //Generar país y capital aleatorio
        int indiceCapital = random.Next(capitalesDisponibles.Count);
        capitalGenerado = capitalesDisponibles[indiceCapital];
        capitalesDisponibles.RemoveAt(indiceCapital);
        PreguntaCapital.Text = "¿De qué país es " + capitalGenerado + "?";

        capitalCorrecta = paisesDisponibles[indiceCapital];
        paisesDisponibles.RemoveAt(indiceCapital);

        //Asignar el valor correcto
        int botonCorrecto = random.Next(4);
        botones[botonCorrecto].Text = capitalCorrecta;

        //asignar valores incorrectos
        for (int i = 0; i < botones.Length; i++)
        {
            if (i != botonCorrecto)
            {
                int indice = random.Next(paisesDisponibles.Count);
                botones[i].Text = paisesDisponibles[indice];
                paisesDisponibles.RemoveAt(indice);
            }
        }
    }
    private async void PaisBtn(object sender, EventArgs e)
    {
        //cuando el usuario haya elegido una opción, el boton Siguiente se habilita
        Next.IsEnabled = true;

        Button button = (Button)sender;
        String respuesta = button.Text;

        if (respuesta == capitalCorrecta)
        {
            respuestaCorrecta++;
            /*DysplayAlert es lo que utilizaremos para que salte por pantalla
              una pestaña si hemos acertado o no.*/
            await DisplayAlert("Correcto", "¡Has acertado, pulse Siguiente para continuar", "Siguiente");
        }
        else
        {
            respuestaIncorrecta++;
            await DisplayAlert("Incorrecto", "La capital de " + capitalGenerado + " es " + capitalCorrecta +
                ", pulse Siguiente para continuar", "OK");
            button.IsEnabled = true;
        }
        foreach (Button btn in botones)
        {
            btn.IsEnabled = false;
        }
    }
    private async void NextBtn(object sender, EventArgs e)
    {
        numPreguntas++;
        MostrarPaisesAleatorios();
        foreach (Button btn in botones)
        {
            btn.IsEnabled = true;
        }
        if (numPreguntas == 5)
        {
            string resultados = $"Has acertado: {respuestaCorrecta}\n" +
                                $"Has fallado: {respuestaIncorrecta}\n" +
                                $"Nota: " + ((float)respuestaCorrecta / numPreguntas) * 10;
            await DisplayAlert("Resultados", resultados, "OK");

            foreach (Button btn in botones)
            {
                btn.IsEnabled = false;
                btn.IsVisible = false;
            }
            Next.IsEnabled = false;
            PreguntaCapital.IsVisible = false;
        }
    }
    private void ResetBtn(object sender, EventArgs e)
    {
        MostrarPaisesAleatorios();
        numPreguntas = 0;
        respuestaCorrecta = 0;
        respuestaIncorrecta = 0;
        Next.IsEnabled = false;
        PreguntaCapital.IsVisible = true;
        foreach (Button btn in botones)
        {
            btn.IsEnabled = true;
            btn.IsVisible = true; 
        }
    }
}