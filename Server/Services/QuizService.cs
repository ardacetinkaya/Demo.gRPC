namespace gRPC.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Data;
    using Grpc.Core;
    using Microsoft.Extensions.Logging;

    public class QuizService : Maths.MathsBase
    {
        private readonly ILogger<QuizService> _logger;
        public QuizService(ILogger<QuizService> logger)
        {
            _logger = logger;
        }
        public override async Task AskQuestion(QuestionRequest questions, IServerStreamWriter<AnswerReply> responseStream, ServerCallContext context)
        {
            foreach (var question in questions.Texts.ToList())
            {
                try
                {
                    if (!string.IsNullOrEmpty(question))
                    {
                        var dt = new DataTable();
                        var answer = Convert.ToDouble(dt.Compute(question, string.Empty));

                        await Task.Delay(800);

                        await responseStream.WriteAsync(new AnswerReply { Answer = answer, Question = question });
                    }
                }
                catch (Exception)
                {

                    await responseStream.WriteAsync(new AnswerReply { Answer = 0, Question = "It seems that something is wrong.!!!" });
                }


            }
        }

        public override async Task<AnswerReply> SolveOperation(QuestionRequest request, ServerCallContext context)
        {
            var dt = new DataTable();
            var answer = Convert.ToDouble(dt.Compute(request.Texts[0], string.Empty));
            return new AnswerReply()
            {
                Question = request.Texts[0].Trim(),
                Answer = answer
            };
        }

    }
}
