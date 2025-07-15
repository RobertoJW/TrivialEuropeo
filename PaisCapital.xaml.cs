using static System.Net.Mime.MediaTypeNames;

namespace TrivialEuropeo;

public partial class PaisCapital : ContentPage
{
    private string? PaisSeleccionado;
    private string? capitalCorrecta;
    private Button[] botones; 
    private Random random = new Random();
    private int respuestaCorrecta = 0;
    private int respuestasIncorrecta = 0;
    private int numeroPregunta = 0; 

    private string[] capitales = {"Berlín", "Viena", "Bruselas", "Sofía", "Nicosia", "Zagreb", "Copenhague",
    "Bratislava", "Liubliana" , "Madrid", "Tallin", "Helsinki", "París", "Atenas", "Budapest", "Dublín",
    "Roma", "Riga", "Vilna", "Luxemburgo", "La Valeta", "Ámsterdam", "Varsovia", "Lisboa", "Londres",
    "Praga", "Bucarest", "Estocolmo"};
    private string[] paises = {"Alemania", "Austria", "Bélgica", "Bulgaria", "Chipre", "Croacia", "Dinamarca",
    "Eslovaquia", "Eslovenia", "España", "Estonia", "Finlandia", "Francia", "Grecia", "Hungría", "Irlanda",
    "Italia", "Letonia", "Lituania", "Luxemburgo", "Malta", "Países Bajos", "Polonia", "Portugal",
    "Reino Unido", "Chéquia", "Rumanía", "Suecia"};

    public PaisCapital()
    {
        InitializeComponent();
        botones = new Button[] {Button1, Button2, Button3, Button4};
        GenerarCapitalYPaisAleatorio();
    }
    private void GenerarCapitalYPaisAleatorio()
    {
        Siguiente.IsEnabled = false; 
        //creamos una lista temporal para evitar repeticiones.
        List<string> capitalesDisponibles = new List<string>(capitales);
        List<string> paisesDisponibles = new List<string>(paises);

        int indiceDelPais = random.Next(paisesDisponibles.Count); //Generamos un país aleatoriamente
        PaisSeleccionado = paisesDisponibles[indiceDelPais];
        capitalCorrecta = capitales[indiceDelPais]; //capital correcta del país
        capitalesDisponibles.RemoveAt(indiceDelPais); //evitamos repeticiones de capitales

        //Eliminamos el pais del Array para evitar repetición
        paisesDisponibles.RemoveAt(indiceDelPais);

        //Asignamos el país a la pregunta
        PreguntaPais.Text = "¿Cuál es la capital de " + PaisSeleccionado + "?";

        //asignar la capital aleatoriamente a los botones
        int indiceBotonCorrecto = random.Next(4);
        botones[indiceBotonCorrecto].Text = capitalCorrecta;

        //asignación de valores incorrectas
        for (int i = 0; i < botones.Length; i ++)
        {
            if (i != indiceBotonCorrecto)
            {
                int indice = random.Next(capitalesDisponibles.Count);
                botones[i].Text = capitalesDisponibles[indice];
                capitalesDisponibles.RemoveAt(indice);
            }
        }
    }
    private async void CapitalBtn(object sender, EventArgs e)
    {
        Siguiente.IsEnabled = true; 
        Button button = (Button)sender;
        String respuesta = button.Text;

        if (respuesta == capitalCorrecta)
        {
            respuestaCorrecta++;
            /*DysplayAlert es lo que utilizaremos para que salte por pantalla
              una pestaña si hemos acertado o no.*/
            await DisplayAlert("Correcto", "¡Has acertado, pulse Siguiente para continuar", "OK"); 
        }
        else
        {
            respuestasIncorrecta++;
            await DisplayAlert("Incorrecto", "La capital de " + PaisSeleccionado + " es " + capitalCorrecta +
                ", pulse Siguiente para continuar", "OK");                                       
        }
        foreach(Button btn in botones)
        {
            btn.IsEnabled = false; 
        }
    }
    private async void SiguienteBtn(object sender, EventArgs e)
    {
        numeroPregunta++;
        GenerarCapitalYPaisAleatorio();
        foreach (Button btn in botones)
        {
            btn.IsEnabled = true;
        }
        if (numeroPregunta == 5)
        {
            string resultados = $"Has acertado: {respuestaCorrecta}\n" +
                                $"Has fallado: {respuestasIncorrecta}\n" +
                                $"Nota: " + ((float)respuestaCorrecta / numeroPregunta) * 10;
            await DisplayAlert("Resultados", resultados, "OK");

            foreach (Button btn in botones)
            {
                btn.IsEnabled = false;
                btn.IsVisible = false;
            }
            Siguiente.IsEnabled = false;
            PreguntaPais.IsVisible = false;
        }
    }
    private void ReiniciarBtn(object sender, EventArgs e)
    {
        GenerarCapitalYPaisAleatorio(); 
        Siguiente.IsEnabled = false; 
        respuestaCorrecta = 0;
        respuestasIncorrecta = 0;
        PreguntaPais.IsVisible = true;
        foreach (Button btn in botones)
        {
            btn.IsEnabled = true;
            btn.IsVisible = true;
        }
    }
}