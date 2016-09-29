using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Band;
using Windows.UI.Core;
using SharpDX.IO;
using SharpDX.Multimedia;
using SharpDX.XAudio2;


namespace Piano
{

    public sealed partial class MainPage : Page
    {
        double xVal;
        double yVal;
        double zVal;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            NavigationCacheMode = NavigationCacheMode.Required;

        }

        private IBandClient bandClient; //Portal into the functionality of the band


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.New)
                return;


            var bands = await BandClientManager.Instance.GetBandsAsync(); //Getting a list of paired Bands.
            var band = bands.FirstOrDefault(); //Connecting to the first band connected
            bandClient = await BandClientManager.Instance.ConnectAsync(band);

            await bandClient.SensorManager.Accelerometer.StartReadingsAsync();
            bandClient.SensorManager.Accelerometer.ReadingChanged += async (sender1, args) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => xVal = Math.Round(Math.Abs(args.SensorReading.AccelerationX * 1000), 1));
                if (xVal > 0 && xVal < 333)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => txt1.Text = "Third Octave");
                    /*if (xVal > 0 && xVal < 333)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note1.Text = "C3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note2.Text = "D3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note3.Text = "E3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note4.Text = "F3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note5.Text = "G3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note6.Text = "A3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note7.Text = "B3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note8.Text = "Db3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note9.Text = "Eb3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note10.Text = "Gb3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note11.Text = "Ab3");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note12.Text = "Bb3");
                    }*/
                }

                else if (xVal > 333 && xVal < 666)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => txt1.Text = "Fourth Octave");

                    /*if (xVal > 333 && xVal < 666)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note1.Text = "C4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note2.Text = "D4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note3.Text = "E4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note4.Text = "F4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note5.Text = "G4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note6.Text = "A4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note7.Text = "B4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note8.Text = "Db4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note9.Text = "Eb4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note10.Text = "Gb4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note11.Text = "Ab4");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note12.Text = "Bb4");
                    }*/
                }

                else if (xVal > 666)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => txt1.Text = "Fifth Octave");
                    /*if (xVal > 666)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note1.Text = "C5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note2.Text = "D5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note3.Text = "E5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note4.Text = "F5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note5.Text = "G5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note6.Text = "A5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note7.Text = "B5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note8.Text = "Db5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note9.Text = "Gb5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note10.Text = "Ab5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note11.Text = "Bb5");
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Note12.Text = "A5");
                    }*/
                }

            };

        }

        private WavePlayer _wavePlayer = new WavePlayer();

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _wavePlayer.AddWave("A48", "Assets/A48_C3.wav");
            _wavePlayer.AddWave("A49", "Assets/A49_Db3.wav");
            _wavePlayer.AddWave("A50", "Assets/A50_D3.wav");
            _wavePlayer.AddWave("A51", "Assets/A51_Eb3.wav");
            _wavePlayer.AddWave("A52", "Assets/A52_E3.wav");
            _wavePlayer.AddWave("A53", "Assets/A53_F3.wav");
            _wavePlayer.AddWave("A54", "Assets/A54_Gb3.wav");
            _wavePlayer.AddWave("A55", "Assets/A55_G3.wav");
            _wavePlayer.AddWave("A56", "Assets/A56_Ab3.wav");
            _wavePlayer.AddWave("A57", "Assets/A57_A3.wav");
            _wavePlayer.AddWave("A58", "Assets/A58_Bb3.wav");
            _wavePlayer.AddWave("A59", "Assets/A59_B3.wav");
            _wavePlayer.AddWave("A60", "Assets/A60_C4.wav");
            _wavePlayer.AddWave("A61", "Assets/A61_Db4.wav");
            _wavePlayer.AddWave("A62", "Assets/A62_D4.wav");
            _wavePlayer.AddWave("A63", "Assets/A63_Eb4.wav");
            _wavePlayer.AddWave("A64", "Assets/A64_E4.wav");
            _wavePlayer.AddWave("A65", "Assets/A65_F4.wav");
            _wavePlayer.AddWave("A66", "Assets/A66_Gb4.wav");
            _wavePlayer.AddWave("A67", "Assets/A67_G4.wav");
            _wavePlayer.AddWave("A68", "Assets/A68_Ab4.wav");
            _wavePlayer.AddWave("A69", "Assets/A69_A4.wav");
            _wavePlayer.AddWave("A70", "Assets/A70_Bb4.wav");
            _wavePlayer.AddWave("A71", "Assets/A71_B4.wav");
            _wavePlayer.AddWave("A72", "Assets/A72_C5.wav");
            _wavePlayer.AddWave("A73", "Assets/A73_Db5.wav");
            _wavePlayer.AddWave("A74", "Assets/A74_D5.wav");
            _wavePlayer.AddWave("A75", "Assets/A75_Eb5.wav");
            _wavePlayer.AddWave("A76", "Assets/A76_E5.wav");
            _wavePlayer.AddWave("A77", "Assets/A77_F5.wav");
            _wavePlayer.AddWave("A78", "Assets/A78_Gb5.wav");
            _wavePlayer.AddWave("A79", "Assets/A79_G5.wav");
            _wavePlayer.AddWave("A80", "Assets/A80_Ab5.wav");
            _wavePlayer.AddWave("A81", "Assets/A81_A5.wav");
            _wavePlayer.AddWave("A82", "Assets/A82_Bb5.wav");
            _wavePlayer.AddWave("A83", "Assets/A83_B5.wav");
            
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A48");
            }
            else if(xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A60");
            }
            else if(xVal > 666)
            {
                _wavePlayer.PlayWave("A72");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A50");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A62");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A74");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A52");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A64");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A76");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A53");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A65");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A77");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A55");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A67");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A79");
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A57");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A69");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A81");
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A59");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A71");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A83");
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A49");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A61");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A73");
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A51");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A63");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A75");
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A54");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A66");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A78");
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A56");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A68");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A80");
            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            if (xVal > 0 && xVal < 333)
            {
                _wavePlayer.PlayWave("A58");
            }
            else if (xVal > 333 && xVal < 666)
            {
                _wavePlayer.PlayWave("A70");
            }
            else if (xVal > 666)
            {
                _wavePlayer.PlayWave("A82");
            }
        }
    }
}
