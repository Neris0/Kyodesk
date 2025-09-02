using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using SistemaChamadosWpf.Models;
using SistemaChamadosWpf.ViewModels;

namespace SistemaChamadosWpf
{
    public partial class MainWindow : Window
    {
        private readonly ChamadosViewModel _vm;
        private readonly Usuario _usuario; // guarda o usu�rio passado no ctor

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario; // mant�m para usar no Loaded

            // Resolve o VM via DI e define DataContext UMA vez
            _vm = ((App)Application.Current).Services.GetRequiredService<ChamadosViewModel>();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // opcional: se tiver algo para o bot�o Detalhes no code-behind
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // informa o usu�rio ao VM e carrega a lista
            _vm.DefinirUsuario(_usuario);          // m�todo s�ncrono do VM
            await _vm.CarregarChamadosAsync();     // popula o grid
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // opcional: manipular sele��o
        }

        private void Deslogar_Click(object sender, RoutedEventArgs e)
        {
            // (opcional) limpar sess�o:
            // var auth = ((App)Application.Current).Services.GetService<SistemaChamadosWpf.Services.AuthService>();
            // auth?.Logout();

            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            var login = new SistemaChamadosWpf.Views.LoginWindow();
            Application.Current.MainWindow = login;
            login.Show();

            Close();
        }
    }
}
