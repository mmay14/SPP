// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        // TODO: Add an appropriate constructor.
	    public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            PrettyPrinter.printBegin(t, n, p);
        }
    }
}

