using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using SistemaChamadosWpf.Data;
using SistemaChamadosWpf.Models;

namespace SistemaChamadosWpf.Views
{
    /// <summary>
    /// Lógica de interação para NovoChamadoWindow.xaml
    /// </summary>
    public partial class NovoChamadoWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly Usuario _usuario;
        public NovoChamadoWindow(Usuario usuario)
        {
            InitializeComponent();
            _context = ((App)Application.Current).Services.GetRequiredService<AppDbContext>();
            _usuario = usuario;
        }
        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            var titulo = TituloTextBox.Text.Trim();
            var descricao = DescricaoTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show("Preencha o título e a descrição.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var chamado = new Chamado
            {
                Titulo = titulo,
                Descricao = descricao,
                UsuarioId = _usuario.Id,
                Status = StatusChamado.Aberto,
                DataCriacao = DateTime.Now
            };
            _context.Chamados.Add(chamado);
            _context.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}