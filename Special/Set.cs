// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
        // TODO: Add an appropriate constructor.
    	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            PrettyPrinter.printSet(t,n,p);
        }
    }
}

