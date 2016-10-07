// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
        // TODO: Add an appropriate constructor.
	    public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            PrettyPrinter.printCond(t, n, p);
        }
    }
}


