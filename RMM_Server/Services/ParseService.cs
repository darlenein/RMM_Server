using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Affinda.API;
using Affinda.API.Models;

namespace RMM_Server.Services
{
    public class ParseService
    {
        private readonly AffindaAPIClient _client;
        public ParseService(string token)

        {
            var credential = new AffindaTokenCredential(token);
            _client = new AffindaAPIClient(credential);

        }
        public Resume CreateResume(string resumePath)
        {

            using (FileStream fs = File.OpenRead(resumePath))

            {
                return _client.CreateResume(file: fs);
            }
        }
    }
}
