#if Unterstützt_Async
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharedTools
{
    public class DistributingTextWriter : TextWriter
    {
        private readonly TextWriter writer1;
        private readonly TextWriter writer2;

        public DistributingTextWriter(TextWriter writer1, TextWriter writer2)
        {
            this.writer1 = writer1;
            this.writer2 = writer2;
        }

        public DistributingTextWriter(TextWriter writer)
        {
            writer1 = Console.Out;
            writer2 = writer;
        }

        public override IFormatProvider FormatProvider
        {
            get
            {
                if (writer1.FormatProvider.Equals(writer2.FormatProvider))
                    return writer1.FormatProvider;
                throw new InvalidOperationException("Unterschiedliche FormatProvider.");
            }
        }

        public override Encoding Encoding
        {
            get
            {
                if (writer1.Encoding.CodePage == writer2.Encoding.CodePage)
                    return writer1.Encoding;
                throw new InvalidOperationException("Unterschiedliche Encodings.");
            }
        }

        public override string NewLine
        {
            get
            {
                if (writer1.NewLine == writer2.NewLine)
                    return writer1.NewLine;
                throw new InvalidOperationException("Unterschiedliche NewLines.");
            }
            set
            {
                writer1.NewLine = value;
                writer2.NewLine = value;
            }
        }

        public override void Close()
        {
            writer1.Close();
            writer2.Close();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {
            writer1.Flush();
            writer2.Flush();
        }

        public override Task FlushAsync()
        {
            return Task.WhenAll(writer1.FlushAsync(), writer2.FlushAsync());
        }

        public override void Write(char value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(char[] buffer)
        {
            writer1.Write(buffer);
            writer2.Write(buffer);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            writer1.Write(buffer, index, count);
            writer2.Write(buffer, index, count);
        }

        public override void Write(bool value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(int value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(uint value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(long value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(ulong value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(float value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(double value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(decimal value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(string value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(object value)
        {
            writer1.Write(value);
            writer2.Write(value);
        }

        public override void Write(string format, object arg0)
        {
            writer1.Write(format, arg0);
            writer2.Write(format, arg0);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            writer1.Write(format, arg0, arg1);
            writer2.Write(format, arg0, arg1);
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            writer1.Write(format, arg0, arg1, arg2);
            writer2.Write(format, arg0, arg1, arg2);
        }

        public override void Write(string format, params object[] arg)
        {
            writer1.Write(format, arg);
            writer2.Write(format, arg);
        }

        public override Task WriteAsync(char value)
        {
            return Task.WhenAll(writer1.WriteAsync(value), writer2.WriteAsync(value));
        }

        public override Task WriteAsync(string value)
        {
            return Task.WhenAll(writer1.WriteAsync(value), writer2.WriteAsync(value));
        }

        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            return Task.WhenAll(writer1.WriteAsync(buffer, index, count), writer2.WriteAsync(buffer, index, count));
        }

        public override void WriteLine()
        {
            writer1.WriteLine();
            writer2.WriteLine();
        }

        public override void WriteLine(char value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            writer1.WriteLine(buffer);
            writer2.WriteLine(buffer);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            writer1.WriteLine(buffer, index, count);
            writer2.WriteLine(buffer, index, count);
        }

        public override void WriteLine(bool value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(uint value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(ulong value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(double value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(decimal value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(string value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            writer1.WriteLine(value);
            writer2.WriteLine(value);
        }

        public override void WriteLine(string format, object arg0)
        {
            writer1.WriteLine(format, arg0);
            writer2.WriteLine(format, arg0);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            writer1.WriteLine(format, arg0, arg1);
            writer2.WriteLine(format, arg0, arg1);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            writer1.WriteLine(format, arg0, arg1, arg2);
            writer2.WriteLine(format, arg0, arg1, arg2);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            writer1.WriteLine(format, arg);
            writer2.WriteLine(format, arg);
        }

        public override Task WriteLineAsync(char value)
        {
            return Task.WhenAll(writer1.WriteLineAsync(value), writer2.WriteLineAsync(value));
        }

        public override Task WriteLineAsync(string value)
        {
            return Task.WhenAll(writer1.WriteLineAsync(value), writer2.WriteLineAsync(value));
        }

        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return Task.WhenAll(writer1.WriteLineAsync(buffer, index, count), writer2.WriteLineAsync(buffer, index, count));
        }

        public override Task WriteLineAsync()
        {
            return Task.WhenAll(writer1.WriteLineAsync(), writer2.WriteLineAsync());
        }

        protected override void Dispose(bool disposing)
        {
            writer1.Dispose();
            writer2.Dispose();
        }
    }
}
#endif