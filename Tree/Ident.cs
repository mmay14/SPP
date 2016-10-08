// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
    public class Ident : Node
    {
        private string name;

        public Ident(string n)
        {
            name = n;
        }

        public override void print(int n)
        {
	        PrettyPrinter.printIdent(n, name);
        }

        public override bool isSymbol()
        {
            return true;
        }

        public override string getName()
        {
            return this.name;
        }

    }
}

