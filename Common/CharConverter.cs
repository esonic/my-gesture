using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Common
{
    internal class CharConverter
    {
        internal static void ProcessChar(char ch)
        {
            switch (ch)
            {
                case ' ':
                    Actions.Key(K.P, Keys.Space);
                    break;
                case 'a':
                    Actions.Key(K.P, Keys.A);
                    break;
                case 'b':
                    Actions.Key(K.P, Keys.B);
                    break;
                case 'c':
                    Actions.Key(K.P, Keys.C);
                    break;
                case 'd':
                    Actions.Key(K.P, Keys.D);
                    break;
                case 'e':
                    Actions.Key(K.P, Keys.E);
                    break;
                case 'f':
                    Actions.Key(K.P, Keys.F);
                    break;
                case 'g':
                    Actions.Key(K.P, Keys.G);
                    break;
                case 'h':
                    Actions.Key(K.P, Keys.H);
                    break;
                case 'i':
                    Actions.Key(K.P, Keys.I);
                    break;
                case 'j':
                    Actions.Key(K.P, Keys.J);
                    break;
                case 'k':
                    Actions.Key(K.P, Keys.K);
                    break;
                case 'l':
                    Actions.Key(K.P, Keys.L);
                    break;
                case 'm':
                    Actions.Key(K.P, Keys.M);
                    break;
                case 'n':
                    Actions.Key(K.P, Keys.N);
                    break;
                case 'o':
                    Actions.Key(K.P, Keys.O);
                    break;
                case 'p':
                    Actions.Key(K.P, Keys.P);
                    break;
                case 'q':
                    Actions.Key(K.P, Keys.Q);
                    break;
                case 'r':
                    Actions.Key(K.P, Keys.R);
                    break;
                case 's':
                    Actions.Key(K.P, Keys.S);
                    break;
                case 't':
                    Actions.Key(K.P, Keys.T);
                    break;
                case 'u':
                    Actions.Key(K.P, Keys.U);
                    break;
                case 'v':
                    Actions.Key(K.P, Keys.V);
                    break;
                case 'w':
                    Actions.Key(K.P, Keys.W);
                    break;
                case 'x':
                    Actions.Key(K.P, Keys.X);
                    break;
                case 'y':
                    Actions.Key(K.P, Keys.Y);
                    break;
                case 'z':
                    Actions.Key(K.P, Keys.Z);
                    break;
                case 'A':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.A);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'B':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.B);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'C':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.C);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'D':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'E':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.E);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'F':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.F);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'G':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.G);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'H':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.H);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'I':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.I);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'J':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.J);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'K':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.K);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'L':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.L);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'M':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.M);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'N':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.N);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'O':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.O);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'P':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.P);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'Q':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Q);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'R':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.R);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'S':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.S);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'T':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.T);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'U':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.U);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'V':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.V);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'W':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.W);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'X':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.X);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'Y':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Y);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case 'Z':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Z);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                ////////////////////////////////////////////////////////////////////////////
                case '0':
                    Actions.Key(K.P, Keys.D0);
                    break;
                case '1':
                    Actions.Key(K.P, Keys.D1);
                    break;
                case '2':
                    Actions.Key(K.P, Keys.D2);
                    break;
                case '3':
                    Actions.Key(K.P, Keys.D3);
                    break;
                case '4':
                    Actions.Key(K.P, Keys.D4);
                    break;
                case '5':
                    Actions.Key(K.P, Keys.D5);
                    break;
                case '6':
                    Actions.Key(K.P, Keys.D6);
                    break;
                case '7':
                    Actions.Key(K.P, Keys.D7);
                    break;
                case '8':
                    Actions.Key(K.P, Keys.D8);
                    break;
                case '9':
                    Actions.Key(K.P, Keys.D9);
                    break;
                /////////////////////////////////////////////////////////////////////////
                case '`':
                    Actions.Key(K.P, Keys.Oemtilde);
                    break;
                case '~':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Oemtilde);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case ',':
                    Actions.Key(K.P, Keys.Oemcomma);
                    break;
                case '<':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Oemcomma);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '.':
                    Actions.Key(K.P, Keys.OemPeriod);
                    break;
                case '>':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemPeriod);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '/':
                    Actions.Key(K.P, Keys.OemQuestion);
                    break;
                case '?':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemQuestion);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case ';':
                    Actions.Key(K.P, Keys.OemSemicolon);
                    break;
                case ':':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemSemicolon);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '\'':
                    Actions.Key(K.P, Keys.OemQuotes);
                    break;
                case '"':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemQuotes);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '[':
                    Actions.Key(K.P, Keys.OemOpenBrackets);
                    break;
                case '{':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemOpenBrackets);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case ']':
                    Actions.Key(K.P, Keys.OemCloseBrackets);
                    break;
                case '}':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemCloseBrackets);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '-':
                    Actions.Key(K.P, Keys.OemMinus);
                    break;
                case '_':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemMinus);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '=':
                    Actions.Key(K.P, Keys.Oemplus);
                    break;
                case '+':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.Oemplus);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '\\':
                    Actions.Key(K.P, Keys.OemPipe);
                    break;
                case '|':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.OemPipe);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                //////////////////////////////////////////////////////////
                case '!':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D1);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '@':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D2);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '#':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D3);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '$':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D4);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '%':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D5);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '^':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D6);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '&':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D7);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '*':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D8);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case '(':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D9);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
                case ')':
                    Actions.Key(K.D, Keys.ShiftKey);
                    Actions.Key(K.P, Keys.D0);
                    Actions.Key(K.U, Keys.ShiftKey);
                    break;
            }
        }
    }
}
