using EmagLite.Client.Services;
using EmagLite.Client.Services.Lucene;
using Lucene.Net.Documents;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuceneController : ControllerBase
    {
        private readonly string filesFolderPath = @"C:\Users\kojoc\source\repos\EmagLite\RESTApi\Data\";
        private readonly string indexFolderPath = @"C:\Users\kojoc\source\repos\EmagLite\RESTApi\Helpers\Lucene\Index\";


        [HttpGet("{searchString}")]
        public IActionResult Search(string searchString)
        {
            FileParser fileParser = new FileParser();
            
            List<Document> documents = new List<Document>();

            foreach (string file in Directory.EnumerateFiles(filesFolderPath, "*.txt"))
            {
                documents.Add(fileParser.ParseDocument(file));
            }

            LuceneService luceneService = new LuceneService(indexFolderPath);
            try
            {
                luceneService.ClearIndex();
                luceneService.BuildCompleteIndex(documents);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // Search
            string[] searchFileds = { "Nume", "Brand", "Tip", "Sistem de operare", "Processor", "Numar nuclee", "Tehnologie" };
            var query_results = luceneService.Search(searchString, searchFileds, 100);

            List<LuceneResult> results = new List<LuceneResult>();
            foreach (var result in query_results)
            {
                LuceneResult match = new LuceneResult()
                {
                    Id = int.Parse(result.Get("Id")),
                    SistemDeOperare = result.Get("Sistem de operare"),
                    Brand = result.Get("Brand"),
                    NumarNuclee = result.Get("Numar nuclee"),
                    Nume = result.Get("Nume"),
                    Processor = result.Get("Processor"),
                    Tehnologie = result.Get("Tehnologie"),
                    Tip = result.Get("Tip"),
                };
                
                results.Add(match);
            }

            return Ok(results);
        }
    }
}
