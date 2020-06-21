# gRPC.Demo
 
Simple demostration to show gRPC services in ASP.NET Core

There are 3 simple projects in this demo repostiory.

- ## Server 
  Main gRPC service which take some inputs as request, process them and return the response and also have another service streams the response.
- ## Client
  Simple Console application that call the ASP.NET Core gRPC Service and get response as a stream data
- ## WWW
  Simple ASP.NET Core Blazor WebAssembly project that make call to ASP.NET Core gRPC Service


Proto file in server as below;

```
syntax = "proto3";

option csharp_namespace = "gRPC.Server";

package Quiz;

service Maths {
  rpc AskQuestion (QuestionRequest) returns (stream AnswerReply);
  rpc SolveOperation (QuestionRequest) returns (AnswerReply);
}


message QuestionRequest {
  repeated string texts = 1;
}

message AnswerReply {
  string question = 1;
  double answer=2;
}

```

Client app. calls the __AskQuestion()__ service; WWW app. calls __SolveQuestion()__ service.
