// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        // TODO: Add an appropriate constructor.
	    public Define() { }

        public override void print(Node t, int n, bool p)
        {
            PrettyPrinter.printDefine(t, n, p);
        }
    }
}


