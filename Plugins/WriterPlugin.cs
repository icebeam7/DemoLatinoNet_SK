using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DemoLatinoNet_SK.Plugins
{
    // Semantic plugins: They are prompt templates designed to work as functions
    public class WriterPlugin
    {
        const string GenerateStoryDefination =
            @"SUBJECT: {{$input}}
	      SUBJETC END
	      Write an interesting short story about the topic provided in 'SUBJECT'.
	      The story must be purely fiction. Do not incorporate real-life events
	      in it.
	      BEGIN STORY:";

        private KernelFunction _generateStoryFunction { get; set; }

        //SEMANTIC FUNCTION
        [KernelFunction, Description("Generate an interesting story")]
        public async Task<string> GenerateStoryAsync(string input, Kernel kernel)
        {
            _generateStoryFunction = KernelFunctionFactory.CreateFromPrompt(GenerateStoryDefination,
                       description: "Generate an interesting story");

            var result = (await _generateStoryFunction.InvokeAsync(kernel, new() { ["input"] = input }).ConfigureAwait(false))
                        .GetValue<string>() ?? string.Empty;

            return result;
        }
    }
}
