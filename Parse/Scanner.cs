// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }
  
        public Token getNextToken()
        {
            int ch;

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.
                ch = In.Read();

                if (ch == -1)
                    return null;
                // Skip white space
               else if (ch == 9 || ch == 10 || ch == 12 || ch == 13 || ch == 32)
                    return getNextToken();
                // Skip comments
                else if ((char)ch == ';')
                {
                    In.ReadLine();
                    return getNextToken();
                }
                // Special characters
                // TODO: make quote recognize ' and (quote)
                else if ((char)ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if ((char)ch == '(')
                    return new Token(TokenType.LPAREN);
                else if ((char)ch == ')')
                    return new Token(TokenType.RPAREN);
                else if ((char)ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);
                
                // Boolean constants
                else if ((char)ch == '#')
                {
                    ch = In.Read();

                    if ((char)ch == 't')
                        return new Token(TokenType.TRUE);
                    else if ((char)ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if ((char)ch == '"')
                {
                    // scan a string into the buffer variable buf
                    int i = 0;
                    ch = In.Read();
                    char c;
                    while ((char)ch != '"')
                    {
                        c = (char)ch;
                        buf[i] = c;
                        i++;
                        ch = In.Read();
                    }
                    return new StringToken(new String(buf, 0, i));
                }

    
                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = ch - '0';
                    int temp;
                    // make sure that the character following the integer
                    // is not removed from the input stream
                    ch = In.Peek();
                    while (ch >= '0' && ch <= '9')
                    {
                        ch = In.Read();
                        temp = ch - '0';
                        i = i*10 + temp;
                        ch = In.Peek();
                    }

                    return new IntToken(i);
                }
        
                // TODO: figure out specifics on identifiers; look up more about identifier rules
                // Identifiers
                else if (((char)ch >= 'A' && (char)ch <= 'Z')
                        || ((char)ch >= 'a' && (char)ch <= 'z')
                         // or ch is some other valid first character
                         // for an identifier
                         // ! $ % & * + - . / : < = > ? @ ^ _ ~
                         || (char)ch == '!' || (char)ch == '$' || (char)ch == '%'
                         || (char)ch == '&' || (char)ch == '*' || (char)ch == '+'
                         || (char)ch == '-' || (char)ch == '.' || (char)ch == '/'
                         || (char)ch == ':' || (char)ch == '<' || (char)ch == '='
                         || (char)ch == '>' || (char)ch == '?' || (char)ch == '@'
                         || (char)ch == '^' || (char)ch == '_' || (char)ch == '~')
                {
                    // scan an identifier into the buffer
                    int i = 0;
                    var c = (char)ch;
                    buf[i] = c;
                    i++;
                    ch = In.Peek();
                    while (((char)ch >= 'A' && (char)ch <= 'Z')
                         || ((char)ch >= 'a' && (char)ch <= 'z')
                         || (char)ch == '!' || (char)ch == '$' || (char)ch == '%'
                         || (char)ch == '&' || (char)ch == '*' || (char)ch == '+'
                         || (char)ch == '-' || (char)ch == '.' || (char)ch == '/'
                         || (char)ch == ':' || (char)ch == '<' || (char)ch == '='
                         || (char)ch == '>' || (char)ch == '?' || (char)ch == '@'
                         || (char)ch == '^' || (char)ch == '_' || (char)ch == '~')
                    {
                        ch = In.Read();
                        c = (char)ch;
                        buf[i] = c;
                        i++;
                        ch = In.Peek();
                    }
                    // make sure that the character following the integer
                    // is not removed from the input stream

                    return new IdentToken(new String(buf, 0, i));
                }
    
                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }

}



