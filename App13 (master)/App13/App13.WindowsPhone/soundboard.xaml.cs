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

namespace App_v1
{

    public sealed partial class soundboard : Page
    {
        public soundboard()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            this.NavigationCacheMode = NavigationCacheMode.Required;
           
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
        private WavePlayer _wavePlayer = new WavePlayer();

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _wavePlayer.AddWave("sound1", "Assets/sound1.wav");
            _wavePlayer.AddWave("sound2", "Assets/sound2.wav");
            _wavePlayer.AddWave("sound3", "Assets/sound3.wav");
            _wavePlayer.AddWave("sound4", "Assets/sound4.wav");
            _wavePlayer.AddWave("sound5", "Assets/sound5.wav");
            _wavePlayer.AddWave("sound6", "Assets/sound6.wav");
            _wavePlayer.AddWave("sound7", "Assets/sound7.wav");
            _wavePlayer.AddWave("sound8", "Assets/sound8.wav");
            _wavePlayer.AddWave("sound9", "Assets/sound9.wav");
            _wavePlayer.AddWave("sound10", "Assets/sound10.wav");
            _wavePlayer.AddWave("sound11", "Assets/sound11.wav");
            _wavePlayer.AddWave("sound12", "Assets/sound12.wav");

            _wavePlayer.AddWave("c", "Assets/c.wav");
            _wavePlayer.AddWave("c#", "Assets/c#.wav");
            _wavePlayer.AddWave("d", "Assets/d.wav");
            _wavePlayer.AddWave("d#", "Assets/d#.wav");
            _wavePlayer.AddWave("e", "Assets/e.wav");
            _wavePlayer.AddWave("f", "Assets/f.wav");
            _wavePlayer.AddWave("f#", "Assets/f#.wav");
            _wavePlayer.AddWave("g", "Assets/g.wav");
            _wavePlayer.AddWave("g#", "Assets/g#.wav");
            _wavePlayer.AddWave("a", "Assets/a.wav");
            _wavePlayer.AddWave("a#", "Assets/a#.wav");
            _wavePlayer.AddWave("b", "Assets/b.wav");

            _wavePlayer.AddWave("lead1", "Assets/lead1.wav");
            _wavePlayer.AddWave("lead2", "Assets/lead2.wav");
            _wavePlayer.AddWave("lead3", "Assets/lead3.wav");
            _wavePlayer.AddWave("lead4", "Assets/lead4.wav");
            _wavePlayer.AddWave("lead5", "Assets/lead5.wav");
            _wavePlayer.AddWave("lead6", "Assets/lead6.wav");
            _wavePlayer.AddWave("lead7", "Assets/lead7.wav");
            _wavePlayer.AddWave("lead8", "Assets/lead8.wav");
            _wavePlayer.AddWave("lead9", "Assets/lead9.wav");
            _wavePlayer.AddWave("lead10", "Assets/lead10.wav");
            _wavePlayer.AddWave("lead11", "Assets/lead11.wav");
            _wavePlayer.AddWave("lead12", "Assets/lead12.wav");

            _wavePlayer.AddWave("bass1", "Assets/bass1.wav");
            _wavePlayer.AddWave("bass2", "Assets/bass2.wav");
            _wavePlayer.AddWave("bass3", "Assets/bass3.wav");
            _wavePlayer.AddWave("bass4", "Assets/bass4.wav");
            _wavePlayer.AddWave("bass5", "Assets/bass5.wav");
            _wavePlayer.AddWave("bass6", "Assets/bass6.wav");
            _wavePlayer.AddWave("bass7", "Assets/bass7.wav");
            _wavePlayer.AddWave("bass8", "Assets/bass8.wav");
            _wavePlayer.AddWave("bass9", "Assets/bass9.wav");
            _wavePlayer.AddWave("bass10", "Assets/bass10.wav");
            _wavePlayer.AddWave("bass11", "Assets/bass11.wav");
            _wavePlayer.AddWave("bass12", "Assets/bass12.wav");
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("c");
            }
            else if(listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound1");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead1");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass1");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("c#");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound2");
            }

            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead2");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass2");
            }
         }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("d");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound3");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead3");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass3");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("d#");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound4");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead4");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass4");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("e");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound5");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead5");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass5");
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("f");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound6");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead6");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass6");
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("f#");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound7");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead7");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass7");
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("g");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound8");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead8");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass8");
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("g#");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound9");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead9");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass9");
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("a");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound10");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead10");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass10");
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("a#");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound11");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead11");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass11");
            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem.Equals(Piano))
            {
                _wavePlayer.PlayWave("b");
            }

            else if (listbox1.SelectedItem.Equals(Drums))
            {
                _wavePlayer.PlayWave("sound12");
            }
            else if (listbox1.SelectedItem.Equals(Lead))
            {
                _wavePlayer.PlayWave("lead12");
            }
            else if (listbox1.SelectedItem.Equals(Bass))
            {
                _wavePlayer.PlayWave("bass12");
            }
        }



        private void advanced_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Advanced));
        }

        private void basic_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        
    }
}
