// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree
{
    public class Nil : Node
    {
        public Nil() { }
  
        public override void print(int n)
        {
            print(n, false);
        }

        public override void print(int n, bool p) {
	        PrettyPrinter.printNil(n, p);
        }

        public override bool isNull()
        {
            return true;
        }
    }
}
