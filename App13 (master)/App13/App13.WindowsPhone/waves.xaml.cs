using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SharpDX;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using Microsoft.Band.Sensors;
using Windows.UI.Core;
using Windows.Devices.Sensors;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App_v1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class waves : Page
    {
      

        double xVal;
        double yVal;
        double zVal;

        public waves()
        {
            this.InitializeComponent();
        }

        private IBandClient bandClient; //Portal into the functionality of the band

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.New)
                return;

            var bands = await BandClientManager.Instance.GetBandsAsync(); //Getting a list of paired Bands.
            var band = bands.FirstOrDefault(); //Connecting to the first band connected
            this.bandClient = await BandClientManager.Instance.ConnectAsync(band);

            bool hrConsentGranted;
            switch (this.bandClient.SensorManager.Accelerometer.GetCurrentUserConsent())
            {
                case UserConsent.Declined:
                    hrConsentGranted = false;
                    break;

                case UserConsent.Granted:
                    hrConsentGranted = true;
                    break;

                default:
                case UserConsent.NotSpecified:
                    hrConsentGranted = await this.bandClient.SensorManager.
                        Accelerometer.RequestUserConsentAsync();
                    break;
            }

            if (hrConsentGranted)
            {
                await this.bandClient.SensorManager.Accelerometer.StartReadingsAsync();
                bandClient.SensorManager.Accelerometer.ReadingChanged += async (sender1, args) =>
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => xAcc.Text = "X: " + (Math.Round(args.SensorReading.AccelerationX * 100, 5)).ToString());
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => yAcc.Text = "Y: " + (Math.Round(args.SensorReading.AccelerationY * 100, 5)).ToString());
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => zAcc.Text = "Z: " + (Math.Round(args.SensorReading.AccelerationZ * 100, 5)).ToString());

                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => textfrequency.Text = "Frequency: " + (Math.Round(args.SensorReading.AccelerationX * 100)).ToString() + "00Hz");

                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => xVal = args.SensorReading.AccelerationX * 100);
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => yVal = args.SensorReading.AccelerationX * 100);
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => zVal = args.SensorReading.AccelerationX * 100);
                };
            }

        }

        //***********************************************************************************************************

        public void FillBuffer(short[] buffer, int sampleRate, double frequency)
        {
            // double ph2 = 2*Math.PI/(sampleRate * 40);
            //double curPh2 = 0.0;
            double fr2 = yVal;

            int totalTime = 0;

            ADSR adsr = new ADSR();

            double enval = 0;

            for (int i = 0; i < buffer.Length - 1; i += 2)
            {
                double time = (double)totalTime / (double)sampleRate;
                enval = adsr.envGenNew(totalTime, 3.3, 5.5, 8.8, 0.03, sampleRate);
                // short currentSample = (short)(Math.Sin(2 * Math.PI * frequency * time) * Math.Sin(fr2 * time) * (double)short.MaxValue); // ampli modu
                short currentSample = (short)((double)enval * (Math.Sin((2 * Math.PI * frequency * time) + 2.0 * Math.Sin(fr2 * time))) * (double)short.MaxValue);// freq modulation
                buffer[i] = currentSample; //(short)(currentSample & 0xFF);
                buffer[i + 1] = currentSample; //(short)(currentSample >> 8);

                totalTime++; //= 2;
                             // curPh2 += ph2;
            }
        }

        private void ButtonSine_Click(object sender, RoutedEventArgs e)
        {
            // Initialization phase, keep this buffer during the life of your application
            // Allocate 10s at 44.1Khz of stereo 16bit signals
            // Middle number = length 

            var myBufferOfSamples = new short[44100 * 5 * 2];

            // Create a DataStream with pinned managed buffer
            var dataStream = DataStream.Create(myBufferOfSamples, true, true);



            var buffer = new AudioBuffer
            {
                Stream = dataStream,
                AudioBytes = (int)dataStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            // Fill myBufferOfSamples
            FillBuffer(myBufferOfSamples, 44100, (int)frequency.Value * 100);

            // PCM 44.1Khz stereo 16 bit format
            var waveFormat = new WaveFormat();

            XAudio2 xaudio = new XAudio2();
            xaudio.StartEngine();
            MasteringVoice masteringVoice = new MasteringVoice(xaudio);
            masteringVoice.SetVolume((int)volume.Value);
            var sourceVoice = new SourceVoice(xaudio, waveFormat, true);


            // Submit the buffer
            sourceVoice.SubmitSourceBuffer(buffer, null);
            sourceVoice.Start();
        }



        //***************************************************************************************************************8




        public void FillBufferSaw(short[] buffer, int sampleRate, double frequency)
        {
            double totalTime = 0;
            double amp = 0;
            int K = (int)Math.Floor(sampleRate * 0.5 / frequency);

            for (int i = 0; i < buffer.Length - 1; i += 2)
            {
                double time = (double)totalTime / (double)sampleRate;
                double csamp = 0;
                for (int k = 1; k < K; k++)
                {
                    amp = Math.Pow(k, -1);
                    amp = amp / Math.PI;
                    csamp += amp * (Math.Sin(2 * Math.PI * k * frequency * time) * (double)short.MaxValue);
                    //short currentSample = (short)(Math.Sin(2*Math.PI*k*frequency*time)*(double) short.MaxValue);

                }

                short currentSample2 = (short)csamp;
                buffer[i] = currentSample2; //(short)(currentSample & 0xFF);
                buffer[i + 1] = currentSample2; //(short)(currentSample >> 8);
                totalTime++; //= 2;
            }

        }



        private void ButtonSaw_Click(object sender, RoutedEventArgs e)
        {
            // Initialization phase, keep this buffer during the life of your application
            // Allocate 10s at 44.1Khz of stereo 16bit signals
            var myBufferOfSamples1 = new short[44100 * 5 * 2];

            // Create a DataStream with pinned managed buffer
            var dataStream = DataStream.Create(myBufferOfSamples1, true, true);

            var buffer = new AudioBuffer
            {
                Stream = dataStream,
                AudioBytes = (int)dataStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            // Fill myBufferOfSamples
            FillBufferSaw(myBufferOfSamples1, 44100, (int)frequency.Value * 100);

            // PCM 44.1Khz stereo 16 bit format
            var waveFormat = new WaveFormat();

            XAudio2 xaudio = new XAudio2();
            xaudio.StartEngine();
            MasteringVoice masteringVoice = new MasteringVoice(xaudio);
            masteringVoice.SetVolume((int)volume.Value);
            var sourceVoice = new SourceVoice(xaudio, waveFormat, true);


            // Submit the buffer
            sourceVoice.SubmitSourceBuffer(buffer, null);
            sourceVoice.Start();
        }

        //*****************************************************************************************************************

        public void FillBufferTri(short[] buffer, int sampleRate, double frequency)
        {
            int totalTime = 0;

            ADSR adsr = new ADSR();

            double enval = 0;

            for (int i = 0; i < buffer.Length - 1; i += 2)
            {
                double time = (double)totalTime / (double)sampleRate;
                enval = adsr.envGenNew(totalTime, 3.3, 5.5, 8.8, 0.03, sampleRate);
                double t = frequency * time + sampleRate;
                short currentSample = (short)((double)enval * (1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f))));

                buffer[i] = currentSample; //(short)(currentSample & 0xFF);
                buffer[i + 1] = currentSample; //(short)(currentSample >> 8);

                totalTime++;//= 2;
            }
        }

        private void ButtonTri_Click(object sender, RoutedEventArgs e)
        {
            // Initialization phase, keep this buffer during the life of your application
            // Allocate 10s at 44.1Khz of stereo 16bit signals
            var myBufferOfSamples1 = new short[44100 * 5 * 2];

            // Create a DataStream with pinned managed buffer
            var dataStream = DataStream.Create(myBufferOfSamples1, true, true);

            var buffer = new AudioBuffer
            {
                Stream = dataStream,
                AudioBytes = (int)dataStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            // Fill myBufferOfSamples
            FillBufferTri(myBufferOfSamples1, 44100, (int)frequency.Value * 100);

            // PCM 44.1Khz stereo 16 bit format
            var waveFormat = new WaveFormat();

            XAudio2 xaudio = new XAudio2();
            xaudio.StartEngine();
            MasteringVoice masteringVoice = new MasteringVoice(xaudio);
            masteringVoice.SetVolume((int)volume.Value);
            var sourceVoice = new SourceVoice(xaudio, waveFormat, true);


            // Submit the buffer
            sourceVoice.SubmitSourceBuffer(buffer, null);
            sourceVoice.Start();
        }


        //********************************************************************************************************





        public void FillBufferSquare(short[] buffer, int sampleRate, double frequency)
        {
            // double ph2 = 2*Math.PI/(sampleRate * 40);
            //double curPh2 = 0.0;
            double fr2 = yVal;

            int totalTime = 0;

            ADSR adsr = new ADSR();

            double enval = 0;

            for (int i = 0; i < buffer.Length - 1; i += 2)
            {
                double time = (double)totalTime / (double)sampleRate;
                enval = adsr.envGenNew(totalTime, 3.3, 5.5, 8.8, 0.03, sampleRate);
                double t = frequency * time + sampleRate;
                // short currentSample = (short)(Math.Sin(2 * Math.PI * frequency * time) * Math.Sin(fr2 * time) * (double)short.MaxValue); // ampli modu
                short currentSample = (short)((double)enval * (Math.Sin((2 * Math.PI * frequency * time) + 2.0 * Math.Sin(fr2 * time))) * (double)short.MaxValue);// freq modulation
                buffer[i] = currentSample; //(short)(currentSample & 0xFF);
                buffer[i + 1] = currentSample; //(short)(currentSample >> 8);

                totalTime++; //= 2;
                             // curPh2 += ph2;
            }

        }


        private void ButtonSqu_Click(object sender, RoutedEventArgs e)
        {
            // Initialization phase, keep this buffer during the life of your application
            // Allocate 10s at 44.1Khz of stereo 16bit signals
            var myBufferOfSamples1 = new short[44100 * 5 * 2];

            // Create a DataStream with pinned managed buffer
            var dataStream = DataStream.Create(myBufferOfSamples1, true, true);

            var buffer = new AudioBuffer
            {
                Stream = dataStream,
                AudioBytes = (int)dataStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            // Fill myBufferOfSamples
            FillBufferSquare(myBufferOfSamples1, 44100, yVal);

            // PCM 44.1Khz stereo 16 bit format
            var waveFormat = new WaveFormat();

            XAudio2 xaudio = new XAudio2();
            xaudio.StartEngine();
            MasteringVoice masteringVoice = new MasteringVoice(xaudio);
            masteringVoice.SetVolume((int)volume.Value);
            var sourceVoice = new SourceVoice(xaudio, waveFormat, true);


            // Submit the buffer
            sourceVoice.SubmitSourceBuffer(buffer, null);
            sourceVoice.Start();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Advanced));
        }



        private void frequency_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            textfrequency.Text = frequency.Value.ToString();
        }

        private void volume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            textvolume.Text = volume.Value.ToString();
        }
    }

}

