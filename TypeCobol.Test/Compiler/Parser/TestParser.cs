﻿using System;
using System.Linq;
using TypeCobol.Compiler;
using TypeCobol.Compiler.Text;

namespace TypeCobol.Test.Compiler.Parser
{
    static class TestParser
    {
        public static void Check_ParserIntegration()
        {
            string textName = "TESTPGM1";

            // Compile test file
            CompilationUnit compilationUnit = ParserUtils.ParseCobolFile(textName);
            
            // The code below enables rich diagnostic if necessary
            //TestErrorListener errorListener = new TestErrorListener();
            //parser.AddErrorListener(errorListener);
            //errorListener.ErrorLog  

            // Check for parsing errors
            if(compilationUnit.CodeElementsDocumentSnapshot.ParserDiagnostics.Count() > 0)
            {            
                throw new Exception(compilationUnit.CodeElementsDocumentSnapshot.ParserDiagnostics.Count() + " errors found while parsing " + textName);
            }
            // Check for semantic analysis errors
            if (compilationUnit.CodeElementsDocumentSnapshot.ParserDiagnostics.Count() != 1 || compilationUnit.CodeElementsDocumentSnapshot.ParserDiagnostics.First().Message != "Data item W-TAB-LCC-CLE-DAT of type date can not be moved into data item W-TAB-LCC-CLE-RIB of type rib")
            {
                throw new Exception("Parser and type checker with TypeCobol extension KO");
            }
        }

        public static void Check_BeforeAfterInsertionBatched() {
            TypeCobol.Test.Compiler.Parser.Multipass.IndexNames names;
            TextChangedEvent e;
            TestUnit unit = new TestUnit("Programs"+System.IO.Path.DirectorySeparatorChar+"Simple");
            unit.comparator = new Multipass(unit.comparator.paths.name);
            unit.Init(new string[] { "*.pgm", "*.cpy", });
            unit.Parse();

            e = updateLine(TextChangeType.LineInserted, 2, "END PROGRAM Simple.");
            e = updateLine(TextChangeType.LineUpdated, 1, "PROGRAM-ID. Simpler.", e);

            // clear document
            unit.compiler.CompilationResultsForProgram.UpdateTextLines(e);
            unit.Parse();

            names = unit.comparator.paths.resultnames as TypeCobol.Test.Compiler.Parser.Multipass.IndexNames;
            names.index = 2;
            System.Console.WriteLine("Compare with result file: "+unit.comparator.paths.result.full);
            unit.Compare();//with Simple.2.txt
        }

        public static void Check_BeforeAfterInsertion() {
            TypeCobol.Test.Compiler.Parser.Multipass.IndexNames names;
            TextChangedEvent e;
            TestUnit unit = new TestUnit("Programs"+System.IO.Path.DirectorySeparatorChar+"Simple");
            unit.comparator = new Multipass(unit.comparator.paths.name);
            unit.Init(new string[] { "*.pgm", "*.cpy", });
            unit.Parse();
            names = unit.comparator.paths.resultnames as TypeCobol.Test.Compiler.Parser.Multipass.IndexNames;
            names.index = 0;
            unit.Compare();//with Simple.0.txt

            // explicitely close program by adding END PROGRAM line
            var e2 = updateLine(TextChangeType.LineInserted, 2, "END PROGRAM Simple.");
            e = updateLine(TextChangeType.LineUpdated, 1, "  PROGRAM-ID. Simple.");
            e.TextChanges.Add(e2.TextChanges[0]);
            unit.compiler.CompilationResultsForProgram.UpdateTextLines(e);
            unit.Parse();
            names.index++;
            System.Console.WriteLine("Compare with result file: "+unit.comparator.paths.result.full);
            unit.Compare();//with Simple.1.txt

            // change program name ; now first and last line have differing program id
            e = updateLine(TextChangeType.LineUpdated, 1, "PROGRAM-ID. Simpler.");
            unit.compiler.CompilationResultsForProgram.UpdateTextLines(e);
            unit.Parse();
            names.index++;
            System.Console.WriteLine("Compare with result file: "+unit.comparator.paths.result.full);
            unit.Compare();//with Simple.2.txt

            // clear document
            e = updateLine(TextChangeType.DocumentCleared, /* the following parameters are not used when DocumentCleared*/ 0, null);
            unit.compiler.CompilationResultsForProgram.UpdateTextLines(e);
            unit.Parse();
            names.index++;
            System.Console.WriteLine("Compare with result file: "+unit.comparator.paths.result.full);
            unit.Compare();//with Simple.3.txt
        }

        private static TextChangedEvent updateLine(TextChangeType type, int line, string text, TextChangedEvent e = null) {
            if (e==null) e = new TextChangedEvent();
            ITextLine snapshot = new TextLineSnapshot(line, text, null);
            e.TextChanges.Add(new TextChange(type, line, snapshot));
            return e;
        }
    }
}
