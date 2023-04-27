using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Services
{
    public class MatchService
    {
        public string RemoveStopWords(string s)
        {
            if(s == "none")
            {
                return "none";
            }
            // Create a new ML context, for ML.NET operations. It can be used for
            // exception tracking and logging, as well as the source of randomness.
            var mlContext = new MLContext();

            // Create an empty list as the dataset. The 'RemoveStopWords' does not
            // require training data as the estimator
            // ('CustomStopWordsRemovingEstimator') created by 'RemoveStopWords' API
            // is not a trainable estimator. The empty list is only needed to pass
            // input schema to the pipeline.
            var emptyData = new List<TextData>();

            // Convert sample list to an empty IDataView.
            var emptyDataView = mlContext.Data.LoadFromEnumerable(emptyData);

            // A pipeline for removing stop words from input text/string.
            // The pipeline first tokenizes text into words then removes stop words.
            // The 'RemoveStopWords' API ignores casing of the text/string e.g. 
            // 'tHe' and 'the' are considered the same stop words.
            var textPipeline = mlContext.Transforms.Text.TokenizeIntoWords("Words","Text", separators: new[] { ' ', '.', ',', '/' })
                .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("WordsWithoutStopWords", "Words", Microsoft.ML.Transforms.Text.StopWordsRemovingEstimator.Language.English));

            // Fit to data.
            var textTransformer = textPipeline.Fit(emptyDataView);

            // Create the prediction engine to remove the stop words from the input
            // text /string.
            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData, TransformedTextData>(textTransformer);

            // Call the prediction API to remove stop words.
            var data = new TextData()
            {
                Text = s
            };

            var prediction = predictionEngine.Predict(data);

            // Print the length of the word vector after the stop words removed.
            //Console.WriteLine("Number of words: " + prediction.WordsWithoutStopWords.Length);

            // Print the word vector without stop words.
            //Console.WriteLine("\nWords without stop words: " + string.Join(",", prediction.WordsWithoutStopWords));
            if (prediction.WordsWithoutStopWords == null)
            {
                return "";
            }
            return string.Join(";", prediction.WordsWithoutStopWords);
        }

        public string NormalizeText(string s)
        {
            var mlContext = new MLContext();
            var emptySamples = new List<TextData>();

            // Convert sample list to an empty IDataView.
            var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

            // A pipeline for normalizing text.
            var normTextPipeline = mlContext.Transforms.Text.NormalizeText("NormalizedText", "Text", TextNormalizingEstimator.CaseMode.Lower, 
                keepDiacritics: false, keepPunctuations: false, keepNumbers: false);

            // Fit to data.
            var normTextTransformer = normTextPipeline.Fit(emptyDataView);

            // Create the prediction engine to get the normalized text from the
            // input text/string.
            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData, TransformedTextData>(normTextTransformer);

            // Call the prediction API.
            var data = new TextData()
            {
                Text = s
            };

            var prediction = predictionEngine.Predict(data);

            // Print the normalized text.
            //Console.WriteLine($"Normalized Text: {prediction.NormalizedText}");
            return prediction.NormalizedText;
        }

        public void ApplyWordEmbedding(string s)
        {
            var mlContext = new MLContext();
            var emptySamples = new List<TextData>();
            var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

            // A pipeline for converting text into a 150-dimension embedding vector using pretrained 'SentimentSpecificWordEmbedding' model.
            // The 'ApplyWordEmbedding' computes the minimum, average and maximum values for each token's embedding vector. Tokens in 
            // 'SentimentSpecificWordEmbedding' model are represented as 50 -dimension vector. Therefore, the output is of 150-dimension [min, avg, max].

            // The 'ApplyWordEmbedding' API requires vector of text as input. The pipeline first normalizes and tokenizes text then applies word embedding transformation.
            var textPipeline = mlContext.Transforms.Text.NormalizeText("Text").Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "Text"))
                .Append(mlContext.Transforms.Text.ApplyWordEmbedding("Features", "Tokens", WordEmbeddingEstimator.PretrainedModelKind.SentimentSpecificWordEmbedding));

            var textTransformer = textPipeline.Fit(emptyDataView);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData, TransformedTextData>(textTransformer);
            var data = new TextData()
            {
                Text = "This is a great product. I would " +
                "like to buy it again."
            };
            var prediction = predictionEngine.Predict(data);

            // Print the length of the embedding vector.
            Console.WriteLine($"Number of Features: {prediction.Features.Length}");

            // Print the embedding vector.
            Console.Write("Features: ");
            foreach (var f in prediction.Features)
                Console.Write($"{f:F4} ");

            //  Expected output:
            //   Number of Features: 150
            //   Features: -1.2489 0.2384 -1.3034 -0.9135 -3.4978 -0.1784 -1.3823 -0.3863 -2.5262 -0.8950 ...
        }

        private class TextData
        {
            public string Text { get; set; }
        }

        private class TransformedTextData : TextData
        {
            public string[] WordsWithoutStopWords { get; set; }
            public string NormalizedText { get; set; }
            public float[] Features { get; set; }
        }
        public string[] TokenizeString(string s)
        {
            s = NormalizeText(s);
            s = RemoveStopWords(s);
            string[] result = s.Split(';');
            return result;
        }
    }
}
