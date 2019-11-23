using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC.Server
{
    public class QuizService : Maths.MathsBase
    {
        private readonly ILogger<QuizService> _logger;
        public QuizService(ILogger<QuizService> logger)
        {
            _logger = logger;
        }
        public override async Task AskQuestion(QuestionRequest request, IServerStreamWriter<AnswerReply> responseStream, ServerCallContext context)
        {
            foreach (var item in request.Texts.ToList())
            {
                try
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        DataTable dt = new DataTable();
                        int answer = (int)dt.Compute(item, string.Empty);

                        await Task.Delay(800);

                        await responseStream.WriteAsync(new AnswerReply { Answer = answer, Question = item });
                    }
                }
                catch (Exception)
                {

                    await responseStream.WriteAsync(new AnswerReply { Answer = 0, Question = "ERROR occured" });
                }


            }
        }

    }
}
