// BoolLit -- Parse tree node class for representing boolean literals

using System;

namespace Tree
{
    public class BoolLit : Node
    {
        private bool boolVal;
  
        public BoolLit(bool b)
        {
            boolVal = b;
        }
  
        public override void print(int n)
        {
	        PrettyPrinter.printBoolLit(n, boolVal);
        }

        public override bool isBool()
        {
            return true;
        }
    }
}
