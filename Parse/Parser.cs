// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp rest
//         |  exp . exp )
//
//
//**Below is the original grammar for rest but needed to be changed to above version
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
	
        private Scanner scanner;

        public Parser(Scanner s) { scanner = s; }
  
        public Node parseExp()
        {
            return parseExp(scanner.getNextToken());
        }

        private Node parseExp(Token tok)
        {
            if (tok == null)
            {
                return null;
            }
            else if (tok.getType() == TokenType.LPAREN)
            {
                return parseRest();
            }
            else if (tok.getType() == TokenType.QUOTE)
            {
                return new Cons(new Ident("quote"), new Cons(parseExp(), new Nil()));
            }
            else if (tok.getType() == TokenType.TRUE)
            {
                return new BoolLit(true);
            }
            else if (tok.getType() == TokenType.FALSE)
            {
                return new BoolLit(false);
            }
            else if (tok.getType() == TokenType.INT)
            {
                return new IntLit(tok.getIntVal());
            }
            else if (tok.getType() == TokenType.STRING)
            {
                return new StringLit(tok.getStringVal());
            }
            else if (tok.getType() == TokenType.IDENT)
            {
                return new Ident(tok.getName());
            }
            else if (tok.getType() == TokenType.DOT)
            {
                Console.Error.WriteLine("Error Parsing: Illegal dot character.");
                return parseExp();
            }
            else if (tok.getType() == TokenType.RPAREN)
            {
                Console.Error.WriteLine("Error Parsing: Illegal right parenthesis.");
                return parseExp();
            }
            else
            {
                return null;
            }
        }

        protected Node parseRest()
        {
            return parseRest(scanner.getNextToken());
        }
  
        private Node parseRest(Token tok)
        {
            //if token is null then the end of file is reached since there is nothing more to read
            if (tok == null)
            {
                Console.Error.WriteLine("Error Parsing Token: End of File");
                return null;
            }

            //if token is a ')' then end of expression is reached and a nil node '()' is returned
            if (tok.getType() == TokenType.RPAREN)
            {
                return new Nil();
            }

         
            //if the token is not null or ')' then it is a exp
            //if the parsed expression is null then end of file is reached because there are no more expressions to read
            if (parseExp(tok).isNull())
            {
                Console.Error.WriteLine("Error Parsing Expression: End file");
                return null;
            }

            //Look ahead part
            Token nextToken = scanner.getNextToken();

            // valid expressions
            //|exp rest
            //|exp . exp )

            if (nextToken.getType() == TokenType.DOT)
            {
                nextToken = scanner.getNextToken();
                if (parseExp(nextToken).isNull())
                {
                    Console.Error.WriteLine("Error Parsing: Missing Exp after period");
                    return null;
                }
                nextToken = scanner.getNextToken();
                if (nextToken.getType() != TokenType.RPAREN)
                {
                    Console.Error.WriteLine("Error Parsing: Missing Right Parenthesis");
                }

                //get the rest of the statement if not a right parenthesis
                while (nextToken != null || nextToken.getType() != TokenType.RPAREN)
                {
                    var exp = parseExp(nextToken);
                    if (exp == null)
                        return null;
                    nextToken = scanner.getNextToken();
                }
            }
            else
            {
                if (parseRest(nextToken) == null)
                    return null;
            }

            //add a cons node with the exp on one branch and the rest on another branch
            return new Cons(parseExp(tok), parseRest(nextToken));
        }
    }
}

