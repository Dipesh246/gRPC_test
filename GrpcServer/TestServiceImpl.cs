using Grpc.Core;
using System.Threading.Tasks;
using System.IO;
using Google.Protobuf;
using System;

namespace GrpcServer
{
    internal class TestServiceImpl : TestService.TestServiceBase
    {
        public override Task<TestResponse> RunTest(TestRequest request, ServerCallContext context)
        {
            string result = $"Test '{request.TestName}' Completed successfully!";
            string filepath = $"Results/{request.TestName}_Result.txt";

            Directory.CreateDirectory("Results");
            File.WriteAllText(filepath, result);
            return Task.FromResult(new TestResponse 
            { 
                Result = result,
                Status = "Success",
            });
        }

        public override Task<FileResponse> GetResultFile(FileRequest request, ServerCallContext context)
        {
            string filepath = $"Results/{request.TestName}_Result.txt";
            
            if(!File.Exists(filepath))
            {
                return Task.FromResult(new FileResponse
                {
                    FileData = ByteString.Empty,
                    FileName = "",
                    Status = "File not found",
                });
            }

            byte[] fileData = File.ReadAllBytes(filepath);
            return Task.FromResult(new FileResponse
            {
                FileData = ByteString.CopyFrom(fileData),
                FileName = Path.GetFileName(filepath),
                Status = "Success",
            });
        }

        public override async Task ProcessTest(IAsyncStreamReader<TestRequest> requestStream, IServerStreamWriter<TestResponse> responseStream, ServerCallContext context)
        {
            try
            {
                while (await requestStream.MoveNext())
                {
                    var request = requestStream.Current;
                    Console.WriteLine($"Received TestName: {request.TestName}, Parameters: {request.Parameters}");
                    await responseStream.WriteAsync(new TestResponse
                    {
                        Result = $"Processed {request.TestName}",
                        Status = "Success",
                    });

                    Console.WriteLine($"Response sent for TestName: {request.TestName}");
                }
            }
            catch (System.Exception ex)
            {
                await responseStream.WriteAsync(new TestResponse
                {
                    Result = ex.Message,
                    Status = "Error",
                });
            }
        }
    }
}
