namespace gRPC.Client
{
    using System;
    using System.Collections.Generic;
    using gRPC.Server;
    using System.Threading;
    using Grpc.Net.Client;
    using System.Runtime.InteropServices;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            string address = "https://localhost:5111";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                address = "http://localhost:5111";
            }

            var channel = GrpcChannel.ForAddress(address);
            var client = new Maths.MathsClient(channel);
            List<string> questions = new List<string>()
            {
                {"22+54"},{"1-1"},{"435-36"},{"6+2*(98*2)"},{"10/2"},{"5-32"},{"4*2"},
                {"1-5*(-10+2)"},{"5+1"},{"15-3"},{"6*2/(9-2)"},{"10/2"},{"5-2"},{"4*2"}
            };

            var questionRequest = new QuestionRequest();
            questionRequest.Texts.AddRange(questions);

            using (var call = client.AskQuestion(questionRequest))
            {
                while (call.ResponseStream.MoveNext(tokenSource.Token).Result)
                {
                    Console.WriteLine($"{call.ResponseStream.Current.Question} = {call.ResponseStream.Current.Answer.ToString()}");
                }

            }
        }
    }
}
