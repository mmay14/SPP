// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
        // TODO: Add an appropriate constructor.
	    public Lambda() { }

        public override void print(Node t, int n, bool p)
        {
            PrettyPrinter.printLambda(t,n,p);
  	}
    }
}

