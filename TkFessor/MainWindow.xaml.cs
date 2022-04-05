using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TkFessor.Controlador;
using TkFessor.Entidades;

namespace TkFessor
{
    public partial class MainWindow : Window
    {
        List<string> UltimosPerfilPesquisados = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            var img = new ImageSourceConverter().ConvertFromString(MudarimagemFundo()) as ImageSource;
            ImagemFundo.Source = img;
        }
        // Imagens de Fundo
        public string MudarimagemFundo()
        {
            Random ImagemAleatoria = new Random();
            string[] listadefotos = new string[5];
            listadefotos[0] = "http://ddragon.leagueoflegends.com/cdn/img/champion/splash/Volibear_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[1] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Garen_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[2] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Gragas_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[3] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Tristana_" + ImagemAleatoria.Next(3).ToString() + ".jpg";
            listadefotos[4] = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ashe_" + ImagemAleatoria.Next(3).ToString() + ".jpg";

            return listadefotos[ImagemAleatoria.Next(5)];
        }

        public void AlterarSolo(string nickname)
        {
            InfoMaestrias.Children.Clear();
            InfoHistorico.Children.Clear();

            ColetarDados executa = new ColetarDados();
            DadosInvocador dadosInvocador = executa.BuscarRequicicao(nickname);

            if (dadosInvocador.id == null)
            {
                Invocador.Text = "Esse Invocador não existe!";
                ImgIcone.Visibility = Visibility.Collapsed;
                Lvl.Text = "";
                Invocador.Margin = new Thickness(-55, 5, 5, 5);

                return;
            }

            List<DadosPerfil> infoFilas = executa.BuscarPerfil(dadosInvocador.id);
            List<DadosMaestria> maestriaInvocador = executa.BuscarMaestria(dadosInvocador.id);
            List<string> chaves = executa.BuscarChaveHistorico(dadosInvocador.puuId);

            var icone = new ImageSourceConverter().ConvertFromString("https://ddragon.leagueoflegends.com/cdn/12.5.1/img/profileicon/" + dadosInvocador.profileIconId + ".png") as ImageSource;
            LinkImg.ImageSource = icone;
            ImgIcone.Visibility = Visibility.Visible;

            Invocador.Margin = new Thickness(5, 5, 5, 5);
            Invocador.Text = dadosInvocador.name;
            Lvl.Text = "Lvl: " + dadosInvocador.summonerLevel;

            for (int i = 0; i < 5; i++)
            {
                CriarLinhasMaestria(maestriaInvocador[i].championId + " " + maestriaInvocador[i].championPoints);
            }

            foreach (var fila in infoFilas)
            {
                if (fila.queueType == "RANKED_SOLO_5x5")
                {
                    InfoEloSolo.Text = "Elo: " + fila.tier + " " + fila.rank;
                    InfoPdlSolo.Text = "PDL: " + fila.leaguePoints;
                    InfoWinSolo.Text = "Wins: " + fila.wins;
                    InfoLosesSolo.Text = "Loses: " + fila.losses;

                    var img = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Emblem_" + fila.tier + ".png") as ImageSource;
                    ImgEloSolo.Source = img;
                }
            }

            foreach (var filaFlex in infoFilas)
            {
                if (filaFlex.queueType == "RANKED_FLEX_SR")
                {
                    InfoEloFlex.Text = "Elo: " + filaFlex.tier + " " + filaFlex.rank;
                    InfoPdlFlex.Text = "PDL: " + filaFlex.leaguePoints;
                    InfoWinFlex.Text = "Wins: " + filaFlex.wins;
                    InfoLosesFlex.Text = "Loses: " + filaFlex.losses;

                    var img = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Emblem_" + filaFlex.tier + ".png") as ImageSource;
                    ImgEloFlex.Source = img;
                }
            }

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    DadosHistorico partidas = executa.BuscarInfoPartida(chaves[i]);
                    var participante = Array.Find(partidas.info.participants, item => item.summonerName == dadosInvocador.name);

                    string resultadoPartida = participante.win ? "Vitória" : "Derrota";

                    CriarLinhasHistorico(participante.championName + " " + participante.lane + " " + partidas.info.gameDuration + " " + resultadoPartida);
                }
            }
            catch
            {
                if (chaves.Count == 0)
                    CriarLinhasHistorico("Esse jogador não possui partidas recentes");
            }
        }
        private void Pesquisarfunc(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                InfoEloSolo.Text = "Elo: ";
                InfoPdlSolo.Text = "PDL: ";
                InfoWinSolo.Text = "Wins: ";
                InfoLosesSolo.Text = "Loses: ";

                InfoEloFlex.Text = "Elo: ";
                InfoPdlFlex.Text = "PDL: ";
                InfoWinFlex.Text = "Wins: ";
                InfoLosesFlex.Text = "Loses: ";

                var img = new ImageSourceConverter().ConvertFromString(MudarimagemFundo()) as ImageSource;
                ImagemFundo.Source = img;

                var imgSolo = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Emblem_Unranked.png") as ImageSource;
                ImgEloSolo.Source = imgSolo;

                var imgFlex = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Emblem_Unranked.png") as ImageSource;
                ImgEloFlex.Source = imgFlex;

                AlterarSolo(BarraPesquisa.Text);

                if (UltimosPerfilPesquisados.Find(item => item.ToLower() == BarraPesquisa.Text.ToLower()) == null && Invocador.Text != "Esse Invocador não existe!")
                {
                    if (UltimosPerfilPesquisados.Count == 8)
                    {
                        UltimosPerfilPesquisados.Remove(UltimosPerfilPesquisados[0]);
                    }

                    UltimosPerfilPesquisados.Add(Invocador.Text);
                }

                PerfilPesquisado.Children.Clear();

                foreach (var perfil in UltimosPerfilPesquisados)
                {
                    CriarLinhas(perfil);
                }

                BarraPesquisa.Text = String.Empty;
            }
        }
        private void CriarLinhasHistorico(string dados)
        {
            TextBlock historico = new TextBlock();

            historico.Text = dados;
            historico.Foreground = Brushes.White;
            historico.FontSize = 16;
            historico.HorizontalAlignment = HorizontalAlignment.Center;
            historico.Margin = new Thickness(10);

            InfoHistorico.Children.Add(historico);
        }
        private void CriarLinhasMaestria(string pontos)
        {
            TextBlock maestria = new TextBlock();

            maestria.Text = pontos;
            maestria.Foreground = Brushes.White;
            maestria.FontSize = 16;
            maestria.HorizontalAlignment = HorizontalAlignment.Center;
            maestria.Margin = new Thickness(10);

            InfoMaestrias.Children.Add(maestria);
        }

        private void CriarLinhas(string nomeInvocador)
        {
            TextBlock perfilName = new TextBlock();

            perfilName.Text = nomeInvocador;
            perfilName.Foreground = Brushes.White;
            perfilName.Opacity = 100;
            perfilName.FontSize = 20;
            perfilName.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ClicaLinha);
            perfilName.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.Hand;
            perfilName.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;

            PerfilPesquisado.Children.Add(perfilName);
        }

        private void ClicaLinha(object sender, MouseButtonEventArgs e)
        {
            TextBlock click = sender as TextBlock;
            AlterarSolo(click.Text);
        }
    }
}