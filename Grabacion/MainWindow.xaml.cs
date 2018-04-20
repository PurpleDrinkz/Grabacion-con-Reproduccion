using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NAudio;
using NAudio.Wave;



namespace Grabacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn waveIn;
        WaveFormat formato;
        WaveFileReader writer;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            formato = new WaveFormat(44100, 1);
            waveIn = new WaveIn();

            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

            writer =
                new WaveFileReader("sonido.wav", formato);
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {


        }

        void OnDataAvailable (object sender, WaveInEventArgs e)
        {

            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;

            for (int i=0; i < bytesGrabados; i++)
            {
                lblMuestra.Text = buffer[i].ToString();
            }

            writer.Write(buffer, 0, bytesGrabados);
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            waveIn.StopRecording();
        }
    }
}
