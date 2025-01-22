using System;
using Grpc.Core;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServer
{
    public partial class Form1 : Form
    {
        private Server _grpcServer;
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Server_Click(object sender, EventArgs e)
        {
            const int Port = 50051;
            Server.Enabled = false;
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                _grpcServer = new Server
                {
                    Services = { TestService.BindService(new TestServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                _grpcServer.Start();
                MessageBox.Show($"Server started on port {Port}");

                await Task.Run(() =>
                {
                    _cancellationTokenSource.Token.WaitHandle.WaitOne();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                StopServer();
                Server.Enabled = true;
            }

        }

        private void StopServer()
        {
            if (_grpcServer != null)
            {
                _grpcServer.ShutdownAsync();
                _cancellationTokenSource.Cancel();
                MessageBox.Show("Server stopped");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopServer();
        }
    }
}
