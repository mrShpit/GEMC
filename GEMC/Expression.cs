namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Expression
    {
        public void Interpret(interpreterContext context)
        {
            List<byte> output = new List<byte>();
            var i = 0;
            try
            {
                while (i < context.Input.Length - 2)
                {
                    if (context.Input[i] == '=' && context.Input[i + 1] == '\r' && context.Input[i + 2] == '\n')
                    {
                        //Skip
                        i += 3;
                    }
                    else if (context.Input[i] == '=')
                    {
                        string shex = context.Input;
                        shex = shex.Substring(i + 1, 2);
                        int hex = Convert.ToInt32(shex, 16);
                        byte b = Convert.ToByte(hex);
                        output.Add(b);
                        i += 3;
                    }
                    else
                    {
                        output.Add((byte)context.Input[i]);
                        i++;
                    }
                }

                context.Output = output;
            }
            catch
            {
                context.Output = null;
            }
        }

        public abstract string UseEncoding(interpreterContext context);
    }

    //Японская кодировка
    public class JapaneseExpression : Expression
    {
        public override string UseEncoding(interpreterContext context)
        {
            return Encoding.GetEncoding("Shift_JIS").GetString(context.Output.ToArray());
        }
    }

    //Стандартная кодировка
    public class DefaultExpression : Expression
    {
        public override string UseEncoding(interpreterContext context)
        {
            if (context.Output != null)
            {
                return Encoding.UTF8.GetString(context.Output.ToArray());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}