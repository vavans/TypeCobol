﻿using System.Collections.Generic;
using TypeCobol.Compiler.CodeElements.Expressions;

namespace TypeCobol.Compiler.CodeElements
{
    /// <summary>
    /// p369: The MOVE statement transfers data from one area of storage to one or more other areas.
    /// </summary>
    public class MoveStatement : CodeElement, TypeCobol.Compiler.CodeModel.SymbolUser
    {
        /// <summary>
        /// identifier-1 , literal-1
        /// The sending area.
        /// </summary>
        private Expression Sending;
        /// <summary>
        /// identifier-2
        /// The receiving areas. identifier-2 must not reference an intrinsic function.
        /// </summary>
        private IList<Identifier> Receiving;
        /// <summary>
        /// CORR is an abbreviation for, and is equivalent to, CORRESPONDING.
        ///
        /// When format 1 (no CORRESPONDING) is specified :
        /// * All identifiers can reference alphanumeric group items, national group items, or
        /// elementary items.
        /// * When one of identifier-1 or identifier-2 references a national group item and the
        /// other operand references an alphanumeric group item, the national group is
        /// processed as a group item; in all other cases, the national group item is
        /// processed as an elementary data item of category national.
        /// * The data in the sending area is moved into the data item referenced by each
        /// identifier-2 in the order in which the identifier-2 data items are specified in the
        /// MOVE statement. See “Elementary moves” on page 370 and “Group moves” on page 374 below.
        ///
        /// When format 2 (with CORRESPONDING) is specified:
        /// * Both identifiers must be group items.
        /// * A national group item is processed as a group item (and not as an elementary
        /// data item of category national).
        /// * Selected items in identifier-1 are moved to identifier-2 according to the rules for
        /// the “CORRESPONDING phrase” on page 281. The results are the same as if
        /// each pair of CORRESPONDING identifiers were referenced in a separate MOVE
        /// statement.
        /// </summary>
        private bool IsCorresponding;

        public MoveStatement(Expression sending, IList<Identifier> receiving, bool corresponding)
            : base(CodeElementType.MoveStatement) {
            this.Sending   = sending;
            this.Receiving = receiving;
            this.IsCorresponding = corresponding;
        }

        public IList<QualifiedName> Symbols {
            get {
                var symbols = new List<QualifiedName>();
                if (Sending is Identifier)
                    symbols.Add(((Identifier)Sending).Name);
                foreach (var identifier in Receiving)
                    symbols.Add(identifier.Name);
                return symbols;
            }
            private set { throw new System.InvalidOperationException(); }
        }
    }
}
