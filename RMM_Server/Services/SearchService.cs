﻿using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Services
{
    public class SearchService
    {
        public List<Research> Search(string keyword, List<Research> rList)
        {
            // Ensures index backward compatibility
            const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

            // Construct a machine-independent path for the index
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var indexPath = Path.Combine(basePath, "index");
            using var dir = FSDirectory.Open(indexPath);

            // Create an analyzer to process the text
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            // Create an index writer
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            using var writer = new IndexWriter(dir, indexConfig);

            foreach (Research r in rList)
            {
                var source = new
                {
                    GUID = Guid.NewGuid().ToString(),
                    Faculty_FirstName = r.Faculty_FirstName.Trim().ToLower(),
                    Faculty_LastName = r.Faculty_LastName.Trim().ToLower(),
                    Description = r.Description.Trim().ToLower(),
                    Research_Name = r.Name.Trim().ToLower(),

                    // condition ? consequent : alt
                    Encouraged_Skills = r.Encouraged_Skills == null ? "" : r.Encouraged_Skills.Trim().ToLower(),
                    Required_Skills = r.Required_Skills == null ? "" : r.Required_Skills.Trim().ToLower(),
                };

                r.GUID = source.GUID;
                var doc = new Document
                {
                    // StringField indexes but doesn't tokenize
                    new StringField("GUID", source.GUID, Field.Store.YES),
                    new TextField("Faculty_FirstName", source.Faculty_FirstName, Field.Store.YES),
                    new TextField("Faculty_LastName", source.Faculty_LastName, Field.Store.YES),
                    new TextField("Description", source.Description, Field.Store.YES),
                    new TextField("Research_Name", source.Research_Name, Field.Store.YES),
                    new TextField("Encouraged_Skills", source.Encouraged_Skills, Field.Store.YES),
                    new TextField("Required_Skills", source.Required_Skills, Field.Store.YES)
                                        
                };
                writer.AddDocument(doc);
            }

            writer.Commit();

            string[] fieldNames = { "GUID", "Faculty_FirstName", "Faculty_LastName", "Description", "Research_Name", "Encouraged_Skills", "Required_Skills" };
            var multiFieldQP = new MultiFieldQueryParser(AppLuceneVersion, fieldNames, analyzer);
            Query query = multiFieldQP.Parse(keyword.Trim().ToLower());

            // Re-use the writer to get real-time updates
            using var reader = writer.GetReader(applyAllDeletes: true);
            var searcher = new IndexSearcher(reader);
            var hits = searcher.Search(query, rList.Count).ScoreDocs;

            foreach (var hit in hits)
            {
                rList.ElementAt(hit.Doc).SearchScore = hit.Score;
            }

            writer.DeleteAll();
            writer.Commit();
            writer.Dispose();
            dir.Dispose();
            return rList;
        }
    }
}
