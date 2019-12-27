using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JabilDesktopApp
{
    /*<summary>
    This application coantanis 1 screen.
    When executing/running the application, the first graphical object, 
    from the top, should be highlighted (drawing a border around it) 
    with a green/blue solid color.
    When a user key down or right (↓ ; →), the green highlighted border should move to the next object.
    When a user key up or left (↑ ; ←), the green highlighted border should move to the previous object.
    </summary>
    */
    public partial class MainWindow : Window
    {
        Border[] arr;
        private int index = 0;
        private int imageCounter = 0;
        int defaultThickness = 3;
        int focusedThickness = 6;
        public MainWindow()
        {         
            InitializeComponent();
            string[] myImages = new string[] {
                "../images/approval-of-proposal.png",
                "../images/design-and-development.png",
                "../images/requirement-analysis.png",
                "../images/testing-and-deployment.png",
            }; // array of the Images
            string imageSeprator = "../imgs/arrows-down.png"; // seprator image between two images.
            Queue<Border> queue = new Queue<Border>(); // declare a queue that stores all the images with border
           
            foreach (string item in myImages)
            {
                Border Br = createBorderObject(item); // add border around only array of images.
                stakepanel.Children.Add(Br); // add the whole Object to the Main Window.
                queue.Enqueue(Br); // add image with border to queue.
                
                if (imageCounter < myImages.Length - 1)
                {
                    Image seprator = createImageObject(imageSeprator, 100, 50);  // create image without using Toolbox.                 
                    stakepanel.Children.Add(seprator); // add to the Main Window.
                }
                imageCounter++;
            }
            arr = queue.ToArray();// convert Queue to Array.
            arr[index].BorderBrush = Brushes.DarkGreen; // first Element highlighted.
            arr[index].BorderThickness = new Thickness(focusedThickness);
        }

        /* Create a border around image object.
          */
        private Border createBorderObject(string url, int cornerRadius = 100, int borderWidth = 120, int borderHeight = 120)
        {
            Image img = createImageObject(url);
            Border Br = new Border();
            Br.BorderBrush = Brushes.Black;
            Br.BorderThickness = new Thickness(defaultThickness);
            Br.Width = borderWidth;
            Br.Height = borderHeight;
            Br.CornerRadius = new CornerRadius(cornerRadius);
            Br.Child = img;
            return Br;
        }
        /* create a image object without using ToolBox.    
         */
        public Image createImageObject(string url, int imageWidth = 100, int imageHeight = 200)
        {
            Image img = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            img.Source = bi;
            img.Width = imageWidth;
            img.Height = imageHeight;
            return img;
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Right || e.Key == Key.Down) && index < arr.Length - 1) //When a user key right or down
            {
                goDown();
            }

            if ((e.Key == Key.Left || e.Key == Key.Up) && index > 0) //When a user key left or up
            {
                goUp();               
            }
        }

        public void goUp() //When a user key left or up, next object highlighted.
        {
            arr[index].BorderBrush = Brushes.Black;
            arr[index].BorderThickness = new Thickness(defaultThickness);
            index--;
            arr[index].BorderBrush = Brushes.DarkGreen;
            arr[index].BorderThickness = new Thickness(focusedThickness);
        }

        public void goDown()//When a user key right or down, next object highlighted.
        {
            arr[index].BorderBrush = Brushes.Black;
            arr[index].BorderThickness = new Thickness(defaultThickness);
            index++;
            arr[index].BorderBrush = Brushes.DarkGreen;
            arr[index].BorderThickness = new Thickness(focusedThickness);
        }
    }
}
