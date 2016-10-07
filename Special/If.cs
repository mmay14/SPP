// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
        // TODO: Add an appropriate constructor.
	    public If() { }

        public override void print(Node t, int n, bool p)
        {
           PrettyPrinter.printIf(t,n,p);
        }
    }
}

