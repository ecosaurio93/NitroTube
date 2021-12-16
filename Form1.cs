using NitroTube.Helpers;
using System.Diagnostics;

namespace NitroTube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void NadaPorAquiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mensaje = "Version 0.1 by kikendo";
        }

        private void CambiarRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var seleccionaRuta = "";
        }
        //aqui ejecutare la accion, en este caso la de descargar
        private async void Button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            await DescargarVideo(this.textBox1.Text ?? "");
        }

        public async Task<bool> DescargarVideo(string urlVideo)
        {
            //Aqui debe cambiarse de forma dinamica la url de momento hardcodeada
            string argumentos = @$" -f 18 {urlVideo}";
            //Aqui puse el ejecutable de youtube-dl
            string fname = @"Helpers\youtube-dl.exe";
            ProcessStartInfo ps = new()
            {
                CreateNoWindow = true,
                FileName = fname,
                Arguments = argumentos,
                RedirectStandardOutput = true
            };
            try
            {
                using (var exeProcess = Process.Start(ps))
                {
                    while (exeProcess != null && exeProcess.Responding && !exeProcess.HasExited && !exeProcess.StandardOutput.EndOfStream)
                    {
                        Thread.Sleep(3000);
                        try
                        {
                            var line = exeProcess.StandardOutput.ReadLine();
                            if (line != null && line.Contains('%') ) // && line.Contains('(') && line.Contains(')'))
                            {
                                //int primerParentesis = line.IndexOf('(');
                                //int segundoParentesis = line.IndexOf(')');
                                //var porcentaje = line[primerParentesis..segundoParentesis];

                                var cadenaPorcentaje = line.Split('%')[0];
                                var stringPorcentaje = cadenaPorcentaje.Split(' ');
                                this.progressBar1.Value = stringPorcentaje[^1].ConvertirEntero() ?? this.progressBar1.Value;

                                Debug.WriteLine(line);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                    }
                    if (exeProcess != null && exeProcess.Responding)
                    {
                        var videoDescargado = true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return false;
            }
        }
    }
}