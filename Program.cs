using System.Text;
using System.Text.Json;
using DemoLatinoNet_SK.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0060

var deployment = "";
var model = "";
var endpoint = "";
var key = "";

var builder = Kernel.CreateBuilder();



builder.Services.AddAzureOpenAIChatCompletion(
    deployment, endpoint, key, model);

builder.Plugins.AddFromType<MathPlugin>();
builder.Plugins.AddFromType<TimePlugin>();
builder.Plugins.AddFromType<CityWeatherPlugin>();
builder.Plugins.AddFromType<WriterPlugin>();


var kernel = builder.Build();

var equation = "4(x - 10) = -6(2 - x) - 6x";


//var result1 = await kernel.InvokePromptAsync(
//    $"Solve the linear equation {equation} reasoning each step carefully. Return the response in json format with the following properties: step");
//Console.WriteLine(result1);
//Console.WriteLine("/////");

//var prompt2 = $"Extract the solution from this text: {result1}. Please only retrieve the numeric value";
//var prompt2 = $"Is the solution from {result1} a prime number";
//var result2 = await kernel.InvokePromptAsync(prompt2);
//Console.WriteLine(result2);


OpenAIPromptExecutionSettings settings =
    new() { MaxTokens = 1000, Temperature = 0.7 };
KernelArguments arguments = new(settings) { {"equation", equation } };
//var result3 = await kernel.InvokePromptAsync(
//    "Solve the linear equation {{$equation}} reasoning each step carefully.",
//    arguments
//    );
//Console.WriteLine(result3);

//Streaming
//var result = kernel.InvokePromptStreamingAsync(
//    "Solve the linear equation {{$equation}} reasoning each step carefully", 
//    arguments);

//await foreach (var update in result)
//{
//    Console.Write(update);
//}

//var prompts = kernel.CreatePluginFromPromptDirectory("Prompts");
//var city = "Prague";
//var response = await kernel.InvokeAsync(
//    prompts["SuggestActivities"],
//    new KernelArguments () { { "destination", city } }
//    );

//Console.WriteLine(response);


//var prompts = kernel.CreatePluginFromPromptDirectory("Prompts");
//string input = "I want to say 'I like to write a letter'";
//string language = "Spanish";

//var response = await kernel.InvokeAsync(prompts["GetTranslation"], new()
//{
//    { "input", input },
//    { "language", language }
//});
//Console.WriteLine($"{response}");

//KernelArguments args2 =
//    new()
//    {
//        { "value", 70 },
//        { "amount", 25 }
//    };
//var sum = await kernel.InvokeAsync("MathPlugin", "Add", args2);
//Console.WriteLine(sum);


OpenAIPromptExecutionSettings s2 = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

var city = "Prague";
KernelArguments args2 = new(s2) { { "city", city } };
//var prompt = "I want to know if it's cloudy in {{$city}} right now";
//var prompt = "What are the current weather conditions in Prague and in Mexico City? " +
//    "I want to know the temperature in Celsius degrees";
var prompt = "Is it raining in Guanajuato or in Rome right now?";

// Get the response from the AI
var result = await kernel.InvokePromptAsync(prompt, args2);
Console.WriteLine(result);

