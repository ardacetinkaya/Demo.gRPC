using System;
using System.Linq;
using System.Collections.Generic;
using gRPC.Server;
using System.Threading;
using Grpc.Net.Client;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace gRPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            string address = "https://localhost:5001";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                address="http://localhost:5001";
            }

            var channel = GrpcChannel.ForAddress(address);
            var client = new Maths.MathsClient(channel);
            List<string> questions = new List<string>()
            {
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"},
                {"2+5"},{"1-1"},{"5-6"},{"6+2*(98*2)"},{"10/2"},{"5-2"},{"4*2"}
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
