using System;
using System.Collections;
using System.Security;
using System.Text;

namespace SharedTools
{
    public static class ConsoleX
    {
        const int indentLevel = 4;

        public static void WriteUnderlined(string text, char c = '=')
        {
            Console.WriteLine(text + Environment.NewLine + new string(c, text.Length));
        }

        public static void WriteIndented(string text, int indent)
        {
            Console.WriteLine(new string(' ', indent) + text);
        }

        public static string Prompt(string prompt, string standard = "")
        {
            if (standard == null) standard = string.Empty;
            string eingabe;

            if (standard == string.Empty)
                Console.Write(prompt + ": ");
            else
                Console.Write(prompt + " (<ENTER> für \"" + standard + "\"): ");
            eingabe = Console.ReadLine();
            return eingabe != string.Empty ? eingabe : standard;
        }

        public static void OverwriteLine(string text, bool newLine = true)
        {
            int links = Console.CursorLeft;
            Console.CursorLeft = 0;
            Console.Write(text);
            if (text.Length < links) Console.Write(new string(' ', links - text.Length));
            if (newLine) Console.WriteLine();
        }

        public static string ReadPassword(char mask = '*', bool newLine = true)
        {
            int links = Console.CursorLeft;
            StringBuilder passwort = new StringBuilder(10);
            ConsoleKeyInfo taste;

            do
            {
                taste = Console.ReadKey(true);

                if (taste.Key == ConsoleKey.Backspace)
                {
                    Console.CursorLeft = links;
                    Console.Write(new string(' ', passwort.Length));
                    passwort.Clear();
                    Console.CursorLeft = links;
                }
                else if (!char.IsControl(taste.KeyChar))
                {
                    Console.Write(mask);
                    passwort.Append(taste.KeyChar);
                }
            } while (taste.Key != ConsoleKey.Enter);
            if (newLine) Console.WriteLine();
            return passwort.ToString();
        }

        public static string PromptPassword(string prompt = "Passwort", char mask = '*', bool newLine = true)
        {
            Console.Write(prompt + ": ");
            return ReadPassword(mask, newLine);
        }

        public static char ReadChar(bool newLine = true)
        {
            char c = Console.ReadKey().KeyChar;
            if (newLine) Console.WriteLine();
            return c;
        }

        public static bool ReadBoolean(bool? standard = null, char trueChar = 'j', char falseChar = 'n', bool newLine = true)
        {
            ConsoleKeyInfo taste;
            char eingabe;

            do
            {
                taste = Console.ReadKey(true);

                if (taste.Key == ConsoleKey.Enter && standard.HasValue)
                    eingabe = standard.Value ? trueChar : falseChar;
                else
                    eingabe = char.ToLower(taste.KeyChar);
            } while (eingabe != trueChar && eingabe != falseChar);
            Console.Write(eingabe);
            if (newLine) Console.WriteLine();
            return eingabe == trueChar;
        }

        public static bool PromptBoolean(string prompt, bool? standard = null, char trueChar = 'j', char falseChar = 'n', bool newLine = true)
        {
            Console.Write(prompt + " [");

            if (!standard.HasValue)
                Console.Write(trueChar + "|" + falseChar + "] ");
            else if (standard.Value)
                Console.Write(char.ToUpper(trueChar) + "|" + falseChar + "] ");
            else
                Console.Write(trueChar + "|" + char.ToUpper(falseChar) + "] ");
            return ReadBoolean(standard, trueChar, falseChar, newLine);
        }

        public static long ReadInt64(bool newLine = true)
        {
            int links = Console.CursorLeft;
            long zahl;
            string zeile = Console.ReadLine();

            while (!long.TryParse(zeile, out zahl))
            {
                Console.CursorTop--;
                Console.CursorLeft = links;
                Console.Write(new string(' ', zeile.Length));
                Console.CursorLeft = links;
                zeile = Console.ReadLine();
            }
            if (!newLine) Console.CursorTop--;
            return zahl;
        }

        public static int ReadInt32(bool newLine = true)
        {
            int links = Console.CursorLeft;
            int zahl;
            string zeile = Console.ReadLine();

            while (!int.TryParse(zeile, out zahl))
            {
                Console.CursorTop--;
                Console.CursorLeft = links;
                Console.Write(new string(' ', zeile.Length));
                Console.CursorLeft = links;
                zeile = Console.ReadLine();
            }
            if (!newLine) Console.CursorTop--;
            return zahl;
        }

        public static short ReadInt16(bool newLine = true)
        {
            int links = Console.CursorLeft;
            short zahl;
            string zeile = Console.ReadLine();

            while (!short.TryParse(zeile, out zahl))
            {
                Console.CursorTop--;
                Console.CursorLeft = links;
                Console.Write(new string(' ', zeile.Length));
                Console.CursorLeft = links;
                zeile = Console.ReadLine();
            }
            if (!newLine) Console.CursorTop--;
            return zahl;
        }

        public static double ReadDouble(bool newLine = true)
        {
            int links = Console.CursorLeft;
            double zahl;
            string zeile = Console.ReadLine();

            while (!double.TryParse(zeile, out zahl))
            {
                Console.CursorTop--;
                Console.CursorLeft = links;
                Console.Write(new string(' ', zeile.Length));
                Console.CursorLeft = links;
                zeile = Console.ReadLine();
            }
            if (!newLine) Console.CursorTop--;
            return zahl;
        }

        public static float ReadSingle(bool newLine = true)
        {
            int links = Console.CursorLeft;
            float zahl;
            string zeile = Console.ReadLine();

            while (!float.TryParse(zeile, out zahl))
            {
                Console.CursorTop--;
                Console.CursorLeft = links;
                Console.Write(new string(' ', zeile.Length));
                Console.CursorLeft = links;
                zeile = Console.ReadLine();
            }
            if (!newLine) Console.CursorTop--;
            return zahl;
        }

        public static void RecursivePrint<T>(T obj)
        {
            RecursivePrint(obj, 0);
        }

        private static void RecursivePrint<T>(T obj, int indent)
        {
            if (!(obj is string) && obj is IEnumerable)
            {
                WriteIndented(obj.GetType().GetFriendlyName(), indent);
                indent += indentLevel;
                foreach (object children in (IEnumerable)obj) RecursivePrint(children, indent);
            }
            else
            {
                WriteIndented(obj.ToString(), indent);
            }
        }
    }

    public class ChangeableLine
    {
        string s;
        int x = Console.CursorLeft;
        int y = Console.CursorTop;

        public ChangeableLine()
        {
            s = string.Empty;
            Console.CursorTop++;
        }

        public ChangeableLine(string text)
        {
            s = text ?? string.Empty;
            Console.WriteLine(text);
        }

        public string Text
        {
            get { return s; }
            set
            {
                if (value == null) value = string.Empty;
                if (value == s) return;
                int previousX = Console.CursorLeft;
                int previousY = Console.CursorTop;
                string neu = value;

                if (s.Length > neu.Length)
                {
                    neu += new string(' ', s.Length - neu.Length);
                }
                int pos;
                int länge = Math.Min(s.Length, neu.Length);

                for (pos = 0; pos < länge; pos++)
                {
                    if (s[pos] != neu[pos]) break;
                }
                s = value;
                Console.SetCursorPosition(x + pos, y);
                if (pos > 0) neu = neu.Substring(pos);
                Console.Write(neu);
                Console.SetCursorPosition(previousX, previousY);
            }
        }
    }

    public class ProgressBar
    {
        private ChangeableLine _zeile;
        private readonly int _width;
        private int _maximum;
        private int _value;

        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException("Maximum");
                if (_value > value) throw new ArgumentOutOfRangeException("Value");

                if (value != _maximum)
                {
                    _maximum = value;
                }
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value < 0 || value > _maximum) throw new ArgumentOutOfRangeException("Value");

                if (value != _value)
                {
                    _value = value;
                    Update();
                }
            }
        }

        private void Update()
        {
            int zeichen = _value * _width / _maximum;
            _zeile.Text = "[" + new string('#', zeichen) + new string('-', _width - zeichen) + "]";
        }

        public ProgressBar(int width)
        {
            _width = width - 2; //Rand abschneiden
            _zeile = new ChangeableLine("[" + new string('-', _width) + "]");
            _maximum = 100;
            _value = 0;
        }

        public ProgressBar() : this(Console.WindowWidth)
        {
        }
    }
}
