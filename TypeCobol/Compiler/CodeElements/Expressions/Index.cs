﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCobol.Compiler.CodeElements.Expressions
{
    public class Index : Expression
    {
        public SymbolReference<IndexName> Name { get; private set; }

        public Index(IndexName indexName)
        {
            Name = new SymbolReference<IndexName>(indexName);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
