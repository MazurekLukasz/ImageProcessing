using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using ImageLibrary;
using System.Diagnostics;

namespace ApplicationWPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        public ICommand OpenImageCommand {get; set;}
        public ICommand ConvertImageCommand {get; set;}
        public ICommand SaveImageCommand { get; set; }

        private string time = string.Empty;
        public string Time
        {
            set
            {
                time = value;
                RaisePropertyChanged("Time");
            }
            get
            {
                return time;
            }
        }

        private bool asynchronous;
        public bool Asynchronous
        {
            set
            {
                asynchronous = value;
                RaisePropertyChanged("Asynchronous");
            }
            get
            {
                return asynchronous;
            }
        }


        private WriteableBitmap outputImage;
        public WriteableBitmap OutputImage
        {
            get { return outputImage; }
            set
            {
                if (value != outputImage)
                {
                    outputImage = value;
                    RaisePropertyChanged("OutputImage");
                }
            }
        }

        private BitmapImage inputImage;
        public BitmapImage InputImage
        {
            get { return inputImage; }
            set
            {
                if (value != inputImage)
                {
                    inputImage = value;
                    RaisePropertyChanged("InputImage");
                }
            }
        }

        #endregion

        public ViewModel()
        {
            OpenImageCommand = new RelayCommand(new Action<object>(LoadImageFromDisc));
            ConvertImageCommand = new RelayCommand(new Action<object>(ConvertImage));
            SaveImageCommand = new RelayCommand(new Action<object>(SaveImageOnDisc));
        }

        #region Methods
        public void LoadImageFromDisc(object obj)
        {
            InputImage = ImageOperations.OpenImageFromDisc();
            if (InputImage == null)
                MessageBox.Show("Image not loaded!", "Warning");
        }

        public void ConvertImage(object obj)
        {
            if (InputImage == null)
            {
                MessageBox.Show("You need to load image first!", "Warning");
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (!Asynchronous)
                ImageProcessing.ToMainColors(InputImage);
            else
             ImageProcessing.ToMainColorsAsync(InputImage);
           

            stopwatch.Stop();
            Time = stopwatch.ElapsedMilliseconds + " ms";
            OutputImage = ImageProcessing.Result;
            
        }

        public void SaveImageOnDisc(object obj)
        {
            if (OutputImage == null)
            {
                if(InputImage == null)
                MessageBox.Show("You need to load and convert image first!", "Warning");
                else
                MessageBox.Show("You need to convert loaded image first!", "Warning");

                return;
            }
   
            ImageOperations.SaveImageOnDisc(OutputImage);
        }

        #endregion

    }
}
