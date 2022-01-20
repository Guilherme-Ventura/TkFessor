using System;
using System.Windows;
using System.Windows.Media;

namespace TkFessor
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var img = new ImageSourceConverter().ConvertFromString(mudarimagemfundo()) as ImageSource;
            ImagemFundo.Source = img;
        }
        // Imagens de Fundo
        public string mudarimagemfundo()
        {
            Random ImagemAleatoria = new Random();
            string[] listadefotos = new string[5];
            listadefotos[0] = "http://ddragon.leagueoflegends.com/cdn/img/champion/splash/Aatrox_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[1] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Garen_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[2] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Gragas_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[3] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Tristana_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[4] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ashe_" + ImagemAleatoria.Next(3).ToString() + ".jpg";

            return listadefotos[ImagemAleatoria.Next(5)];

        }
    }
}