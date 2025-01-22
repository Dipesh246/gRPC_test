using System;
using Grpc.Core;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace GrpcClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Client_Click(object sender, EventArgs e)
        {
            const string Address = "localhost:50051";

            Channel channel = null;
            channel = new Channel(Address, ChannelCredentials.Insecure);
            var client = new TestService.TestServiceClient(channel);

            var call = client.ProcessTest();

            _ = Task.Run(async () =>
            {
                //for (int i = 0; i < 5; i++)
                //{
                    await call.RequestStream.WriteAsync(new TestRequest
                    {
                        TestName = $"Test",
                        Parameters = $"Parameter"
                    });
                    Console.WriteLine($"Sent Test to sesrver.");
                    await Task.Delay(1000);
                //}
                //await call.RequestStream.CompleteAsync();
            });

            try
            {
                var testResponse = await Task.Run(() =>
                client.RunTest(new TestRequest { TestName = "SampleTest", Parameters = "Param1=Value1" })
                );
                MessageBox.Show($"Test Result: {testResponse.Result}", "Test Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var fileResponse = await Task.Run(() =>
                client.GetResultFile(new FIleRequest { TestName = "SampleTest" })
                );

                if (fileResponse.Status == "Success")
                {
                    string localFilePath = Path.Combine(Environment.CurrentDirectory, fileResponse.FileName);
                    File.WriteAllBytes(localFilePath, fileResponse.FileData.ToByteArray());
                    MessageBox.Show($"File saved to: {localFilePath}", "File Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show($"Error retriving file: {fileResponse.Status}", "File Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                while (await call.ResponseStream.MoveNext())
                {
                    var response = call.ResponseStream.Current;
                    MessageBox.Show($"Received response: {response.Result}, Status: {response.Status}");
                }
                call.Dispose();
            }
            catch (RpcException rpcEx)
            {
                MessageBox.Show($"Error: {rpcEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (channel != null)
                {
                    try
                    {
                        await channel.ShutdownAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error during channel shutdown: {ex.Message}", "Shutdown Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
