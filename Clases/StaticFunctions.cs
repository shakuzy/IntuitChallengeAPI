using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IntuitChallengeAPI.Clases
{
    public static class StaticFunctions
    {
        public static string Desencriptar(object DataValue)
        {
            long X;
            string Temp = "";
            string HexByte;
            var loopTo = Strings.Len(DataValue);
            for (X = 1; X <= loopTo; X += 2)
            {
                HexByte = Strings.Mid(Conversions.ToString(DataValue), Conversions.ToInteger(X), 2);
                Temp = Temp + (char)ConvToInt(HexByte);
            }
            // retorno
            return Temp;
        }

        private static int ConvToInt(string X)
        {
            string x1;
            string x2;
            int Temp;

            x1 = Strings.Mid(X, 1, 1);
            x2 = Strings.Mid(X, 2, 1);

            if (Information.IsNumeric(x1))
                Temp = 16 * int.Parse(x1);
            else
                Temp = (Strings.Asc(x1) - 55) * 16;

            if (Information.IsNumeric(x2))
                Temp = Temp + int.Parse(x2);
            else
                Temp = Temp + (Strings.Asc(x2) - 55);

            // retorno
            return Temp;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            var method = sf.GetMethod();
            return method == null ? "NULL" : method.Name;
        }
    }
}
