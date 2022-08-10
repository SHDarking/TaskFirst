using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst
{
    internal class Indexer
    {
        public List<string> FilesInIndex { get; private set; } = new();
        private static Indexer _indexer = null;
        private Indexer() { }

        public static Indexer GetInstanceIndexer() 
        {
            if (_indexer == null)
            {
                _indexer = new Indexer();
            }
            return _indexer;
        }


    }
}
