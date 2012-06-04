using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Powerhourer.BusinessLayer;

namespace Powerhourer {
    public partial class PlayingPage : Page {

        PowerhourService powerhourService;
        
        public PlayingPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            powerhourService = (PowerhourService)e.Content;
        }



        private void btnPausePlay_Click(object sender, RoutedEventArgs e)
        {
            if (powerhourService.IsPlaying) {
                powerhourService.Pause();
                btnPausePlay.Content = "Start this back up, I'm threw crying!";
            }
            else {
                powerhourService.Resume();
                btnPausePlay.Content = "I'm weak and need a timeout...";
            }
        }


        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you are giving up?  Once you do, you have to start from the beginning!", 
                "Confirmation", 
                MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK) {
                powerhourService.Stop();
            }
        }
    }
}
