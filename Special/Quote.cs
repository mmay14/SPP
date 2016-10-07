// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
        // TODO: Add an appropriate constructor.
    	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            PrettyPrinter.printQuote(t,n,p);
        }
    }
}

