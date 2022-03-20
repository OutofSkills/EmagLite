using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.QueryParsers.Classic;
using System.Collections.Generic;
using System;
using Lucene.Net.Util;

namespace EmagLite.Client.Services
{
    public class LuceneService
    {
        private Analyzer _analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        private string _indexPath;
        private Directory _indexDirectory;
        private IndexWriter _indexWriter;
        private IndexWriterConfig _indexWriterConfig;


        public LuceneService(string indexPath)
        {
            this._indexPath = indexPath;
            _indexDirectory = new MMapDirectory(new System.IO.DirectoryInfo(_indexPath));
            _indexWriterConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer);
        }

        public void BuildCompleteIndex(IEnumerable<Document> documents)
        {
            if (_indexWriter is null)
                _indexWriter = new IndexWriter(_indexDirectory, _indexWriterConfig);

            foreach (var doc in documents)
            {
                _indexWriter.AddDocument(doc);
            }

            _indexWriter.Flush(true, true);
            _indexWriter.Commit();
            _indexWriter.Dispose();
        }

        public int UpdateIndex(IEnumerable<Document> documents)
        {
            throw new NotImplementedException();
        }

        public void ClearIndex()
        {
            _indexWriter = new IndexWriter(_indexDirectory, _indexWriterConfig);
            _indexWriter.DeleteAll();
            _indexWriter.Commit();
            //_indexWriter.Dispose();
        }


        //Single field search
        public IEnumerable<Document> Search(string searchTerm, string searchField, int limit)
        {
            DirectoryReader ireader = DirectoryReader.Open(_indexDirectory);
            var searcher = new IndexSearcher(ireader);
            var parser = new QueryParser(LuceneVersion.LUCENE_48, searchField, _analyzer);
            var query = parser.Parse(searchTerm);
            var hits = searcher.Search(query, limit).ScoreDocs;

            var documents = new List<Document>();
            foreach (var hit in hits)
            {
                documents.Add(searcher.Doc(hit.Doc));
            }

            //_analyzer.Close();
            //searcher.Dispose();
            return documents;
        }

        //Allows multiple field searches
        public IEnumerable<Document> Search(string searchTerm, string[] searchFields, int limit)
        {
            DirectoryReader ireader = DirectoryReader.Open(_indexDirectory);
            var searcher = new IndexSearcher(ireader);
            var parser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, searchFields, _analyzer);

            parser.DefaultOperator = Operator.OR;
            var query = parser.Parse(searchTerm);
            var hits = searcher.Search(query, limit).ScoreDocs;

            var documents = new List<Document>();
            foreach (var hit in hits)
            {
                documents.Add(searcher.Doc(hit.Doc));
            }

            return documents;
        }

    }
}
