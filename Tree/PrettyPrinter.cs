using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Tree
{
    class PrettyPrinter
    {
        public static void printBegin(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.WriteLine("(begin");
                printRest(node, Math.Abs(indent) + 4);
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printLambda(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.Write("(lambda");
                Node cdr = node.getCdr();
                if (cdr.isPair())
                {
                    Console.Write(' ');
                    cdr.getCar().print( -(Math.Abs(indent) + 4), false);
                    Console.WriteLine();
                    printRest(cdr, Math.Abs(indent) + 4);
                }
                else
                {
                    printRest(node, -(Math.Abs(indent) + 4));
                    Console.WriteLine();
                }
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printBoolLit(int indent, bool boolValue)
        {
            indention(indent);
            if (boolValue)
                Console.Write("#t");
            else
                Console.Write("#f");
            terminate(indent);
        }

        public static void printIntLit(int indent, int intVal)
        {
            indention(indent);
            Console.Write(intVal);
            terminate(indent);
        }

        public static void printStringLit(int indent, string stringVal)
        {
            indention(indent);
            Console.Write("\"" + stringVal + "\"");
            terminate(indent);
        }

        public static void printIdent(int indent, string name)
        {
            indention(indent);
            Console.Write(name);
            terminate(indent);
        }

        public static void printNil(int indent, bool hasLeftParen)
        {
            indention(indent-4);
            if (hasLeftParen)
                Console.Write(")");
            else
                Console.Write("()");
            terminate(indent);
        }

        public static void printQuote(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                Node cdr = node.getCdr();
                if (cdr.isPair())
                {
                    indention(indent);
                    Console.Write('\'');
                    cdr.getCar().print(-(Math.Abs(indent) + 1), false);
                    terminate(indent);
                }
                else
                    printRegular(node, indent, hasLeftParen);
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printIf(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.Write("(if");
                Node cdr = node.getCdr();
                if (cdr.isPair())
                {
                    Console.Write(' ');
                    cdr.getCar().print(-(Math.Abs(indent) + 4), false);
                    Console.WriteLine();
                    printRest(cdr, Math.Abs(indent) + 4);
                }
                else
                {
                    printRest(node, -(Math.Abs(indent) + 4));
                    Console.WriteLine();
                }
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printLet(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.WriteLine("(let");
                printRest(node, Math.Abs(indent) + 4);
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printCond(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.WriteLine("(cond");
                printRest(node, Math.Abs(indent) + 4);
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printDefine(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.Write("(define");
                Node cdr = node.getCdr();
                if (cdr.isPair())
                {
                    Node car = cdr.getCar();
                    if (car.isPair())
                    {
                        Console.Write(' ');
                        car.print( -(Math.Abs(indent) + 4), false);
                        Console.WriteLine();
                        printRest(cdr, Math.Abs(indent) + 4);
                    }
                    else
                    {
                        printRegular(cdr, -(Math.Abs(indent) + 4), true);
                        Console.WriteLine();
                    }
                }
                else
                {
                    printRest(node, -(Math.Abs(indent) + 4));
                    Console.WriteLine();
                }
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printSet(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.Write("(set!");
                printRest(node, -(Math.Abs(indent) + 4));
                Console.WriteLine();
            }
            else
                printRegular(node, indent, hasLeftParen);
        }

        public static void printRegular(Node node, int indent, bool hasLeftParen)
        {
            if (!hasLeftParen)
            {
                indention(indent);
                Console.Write('(');
                node.getCar().print(-(Math.Abs(indent) + 4), false);
                printRest(node, -(Math.Abs(indent) + 4));
                terminate(indent);
            }
            else
            {
                if (indent < 0)
                    Console.Write(' ');
                node.getCar().print(indent, false);
                printRest(node, indent);
            }
        }

        private static void indention(int indent)
        {
            for (int i = 0; i < indent; ++i)
            {
                Console.Write(" ");
            }
        }

        private static void printRest(Node node, int indent)
        {
            Node cdr = node.getCdr();
            if (cdr.isNull() || cdr.isPair())
            {
                cdr.print(indent, true);
            }
            else
            {
                if (indent >= 0)
                    indention(indent);
                else
                    Console.Write(' ');
                Console.Write(". ");
                cdr.print(-Math.Abs(indent), false);
                if (indent >= 0)
                {
                    Console.WriteLine();
                    indention(indent - 4);
                }
                Console.Write(')');
                terminate(indent);
            }
        }

        private static void terminate(int indent)
        {
            if (indent < 0)
                return;
            Console.WriteLine();
        }
    }
}
