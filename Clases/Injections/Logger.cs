using System.Runtime.CompilerServices;

namespace IntuitChallengeAPI.Clases.Injections
{
    public class Logger : ILogger
    {
        public Logger()
        {

        }

        public async Task GuardarError(string message, [CallerMemberName]string? callerName = "")
        {
            try
            {
                string dia = DateTime.Now.Year.ToString() + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day);

                string hora = string.Format("{0:00}", DateTime.Now.Hour);

                string nombre = dia + "_" + hora + ".txt";

                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Errores") == false)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Errores");

                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Errores\" + dia) == false)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Errores\" + dia);

                System.IO.StreamWriter archivo = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Errores\" + dia + @"\" + nombre, true);


                var mensaje = "";
                mensaje = "[" + DateTime.Now.ToShortDateString() + "   " + DateTime.Now.ToLongTimeString() + "] " + message + " | Metodo: " + callerName;

                await archivo.WriteLineAsync();
                await archivo.WriteLineAsync(mensaje);
                archivo.Close();
                await GuardarLog(mensaje, callerName, true);
            }
            catch (Exception)
            {
            }
        }

        public async Task GuardarImportante(string message, [CallerMemberName] string? callerName = "")
        {
            try
            {
                string dia = DateTime.Now.Year.ToString() + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day);

                string hora = string.Format("{0:00}", DateTime.Now.Hour);

                string nombre = dia + "_" + hora + ".txt";

                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Logs") == false)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Logs");

                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Logs\" + dia) == false)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Logs\" + dia);

                System.IO.StreamWriter archivo = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Logs\" + dia + @"\" + nombre, true);


                var log = "[" + DateTime.Now.ToShortDateString() + "   " + DateTime.Now.ToLongTimeString() + "] " + message + " | Metodo: " + callerName;

                await archivo.WriteLineAsync();
                await archivo.WriteLineAsync(log);
                archivo.Close();
                await GuardarLog(log, callerName, true);
            }
            catch (Exception)
            {
            }
        }

        public async Task GuardarLog(string message, string callerName, bool DesdeOtroLog = false)
        {
            try
            {
                var nombre = "Estados.txt";

                if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Estados") == false)
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Estados");



                string dir = System.AppDomain.CurrentDomain.BaseDirectory + @"\SrvLogs\Estados\" + nombre;

                if (File.Exists(dir))
                {
                    FileInfo f = new FileInfo(dir);

                    if (f.Length > (500 * 1024)) /*5 Mb*/
                    {
                        File.Delete(dir);
                    }

                }
                System.IO.StreamWriter archivo = new System.IO.StreamWriter(dir, true);
                var str = message;
                if (DesdeOtroLog == false)
                {
                    str = "[" + DateTime.Now.ToShortDateString() + "   " + DateTime.Now.ToLongTimeString() + "] " + message;
                }

                await archivo.WriteLineAsync();
                await archivo.WriteLineAsync(str);
                archivo.Close();
            }
            catch (Exception)
            {
            }
        }
    }


}
