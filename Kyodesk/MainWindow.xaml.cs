using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using SistemaChamadosWpf.Models;
using SistemaChamadosWpf.ViewModels;

namespace SistemaChamadosWpf
{
    public partial class MainWindow : Window
    {
        private readonly ChamadosViewModel _vm;
        private readonly Usuario _usuario; // guarda o usuário passado no ctor

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario; // mantém para usar no Loaded

            // Resolve o VM via DI e define DataContext UMA vez
            _vm = ((App)Application.Current).Services.GetRequiredService<ChamadosViewModel>();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // opcional: se tiver algo para o botão Detalhes no code-behind
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // informa o usuário ao VM e carrega a lista
            _vm.DefinirUsuario(_usuario);          // método síncrono do VM
            await _vm.CarregarChamadosAsync();     // popula o grid
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // opcional: manipular seleção
        }

        private void Deslogar_Click(object sender, RoutedEventArgs e)
        {
            // (opcional) limpar sessão:
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
