using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.IO;

namespace Project_ChangeBitmapRealtime_Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string PathImagesDinoShow = @"C:\DinoShow\";
        string PathImagesDinoUse = @"C:\DinoUse\";

        string PathImages = @"C:\Users\takatanonly\Documents\Visual Studio 2010\Projects\Project_ChangeBitmapRealtime_Test1\Project_ChangeBitmapRealtime_Test1\bin\Debug\Images\";
        string PathImagestest = @"C:\Users\takatanonly\Documents\Visual Studio 2010\Projects\Project_ChangeBitmapRealtime_Test1\Project_ChangeBitmapRealtime_Test1\Images\";
        
        Image[] imagesDino1 = new Image[6] { new Image(), new Image(), new Image(), new Image(), new Image(), new Image() };
        Image[] imagesDino2 = new Image[6] { new Image(), new Image(), new Image(), new Image(), new Image(), new Image() };

        Image[] imagesEnvironment = new Image[30] { new Image(), new Image(), new Image(),new Image(), new Image(),new Image(), new Image(), new Image(),new Image(), new Image(),
        new Image(), new Image(), new Image(),new Image(), new Image(),new Image(), new Image(), new Image(),new Image(), new Image(),
        new Image(), new Image(), new Image(),new Image(), new Image(),new Image(), new Image(), new Image(),new Image(), new Image()};


        bool[] images_direction = new bool[30];
        bool[] angle_direction = new bool[30];

        DispatcherTimer[] timer_UpdateRotate = new DispatcherTimer[6] {
        new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer()};

        DispatcherTimer[] timer_UpdateCharacters = new DispatcherTimer[30] {
        new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(),
        new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(),
        new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer(), new DispatcherTimer()};
        
        int count = 0, countTemp = 0;
        double angle = 0, angle2 = 0, angle3 = 0 , angle4 = 0;
        double angle5 = 0, angle6 = 0, angle7 = 0, angle8 = 0;
        bool left = true, right = false;

        BitmapSource trex_leg1, trex_leg2, trex_body, trex_arm1, trex_arm2, trex_head;
        BitmapSource trex_leg1_flip, trex_leg2_flip, trex_body_flip, trex_arm1_flip, trex_arm2_flip, trex_head_flip;

        BitmapSource leg1PartDino1, leg2PartDino1, bodyPartDino1, headPartDino1, arm1PartDino1, arm2PartDino1;
        BitmapSource leg1_flipPartDino1, leg2_flipPartDino1, body_flipPartDino1, head_flipPartDino1, arm1_flipPartDino1, arm2_flipPartDino1;

        BitmapSource leg1PartDino2, leg2PartDino2, bodyPartDino2, headPartDino2, arm1PartDino2, arm2PartDino2;
        BitmapSource leg1_flipPartDino2, leg2_flipPartDino2, body_flipPartDino2, head_flipPartDino2, arm1_flipPartDino2, arm2_flipPartDino2;

        int[] locationX_dino = new int[30];
        int[] locationY_dino = new int[30];

        double[] angleHead_dino = new double[30];
        double[] angleBody_dino = new double[30];
        double[] angleArm1_dino = new double[30];
        double[] angleArm2_dino = new double[30];
        double[] angleLeg1_dino = new double[30];
        double[] angleLeg2_dino = new double[30];

        int head = 0, arm1 = 1, leg1 = 2, body = 3, arm2 = 4 , leg2 = 5;
        int imageDino_width = 2480 / 8;
        int imageDino_height = 3508 / 8;

        String DinoFileName;

        public MainWindow()
        {

            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\takatanonly\\Documents\\Visual Studio 2010\\Projects\\Project_ChangeBitmapRealtime_Test1\\Project_ChangeBitmapRealtime_Test1\\Images\\BG.jpg"));
            this.Background = myBrush;
            
            //environment set image
            imagesEnvironment[0].Width = 500;
            imagesEnvironment[0].Height = 500;
            imagesEnvironment[0].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            imagesEnvironment[0].Source = new BitmapImage(new Uri(PathImagestest + "tree.png"));
            imagesEnvironment[0].Margin = new System.Windows.Thickness(-imagesEnvironment[0].Width / 2, 400, 0, 0);

            imagesEnvironment[1].Width = 500;
            imagesEnvironment[1].Height = 500;
            imagesEnvironment[1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            imagesEnvironment[1].Source = new BitmapImage(new Uri(PathImagestest + "tree.png"));
            imagesEnvironment[1].Margin = new System.Windows.Thickness(600, -500, 0, 0);

            imagesEnvironment[2].Width = 500;
            imagesEnvironment[2].Height = 500;
            imagesEnvironment[2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            imagesEnvironment[2].Source = new BitmapImage(new Uri(PathImagestest + "tree.png"));
            imagesEnvironment[2].Margin = new System.Windows.Thickness(1920 - imagesEnvironment[2].Width / 2, 400, 0, 0);

            //tree add
            ContentRoot.Children.Add(imagesEnvironment[1]);
            ContentRoot.Children.Add(imagesEnvironment[0]);
            ContentRoot.Children.Add(imagesEnvironment[2]);

            timer_UpdateRotate[0].Tick += new EventHandler(Timer_timer_UpdateRotate);
            timer_UpdateRotate[0].Interval = new TimeSpan(0, 0, 0, 0, 15);
            //timer_UpdateRotate[0].Start();

            timer_UpdateRotate[1].Tick += new EventHandler(Timer_timer_UpdateRotate2);
            timer_UpdateRotate[1].Interval = new TimeSpan(0, 0, 0, 0, 6);
            //timer_UpdateRotate[1].Start();

            timer_UpdateRotate[2].Tick += new EventHandler(Timer_timer_UpdateRotate3);
            timer_UpdateRotate[2].Interval = new TimeSpan(0, 0, 0, 0, 10);

            timer_UpdateRotate[3].Tick += new EventHandler(Timer_timer_UpdateRotate4);
            timer_UpdateRotate[3].Interval = new TimeSpan(0, 0, 0, 0, 4);

            
            System.Windows.Threading.DispatcherTimer timer_UpdateFile = new System.Windows.Threading.DispatcherTimer();
            timer_UpdateFile.Tick += new EventHandler(Timer_Tick_UpdateFile);
            timer_UpdateFile.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer_UpdateFile.Start();
        }

        private void Timer_timer_UpdateRotate(object sender, EventArgs e)
        {
            if (angleHead_dino[0] > 3)
                angle_direction[0] = left;

            else if (angleHead_dino[0] < -3)
                angle_direction[0] = right;

            if (angle_direction[0] == right)
            {
                angleHead_dino[0] += 0.1;
                angleLeg1_dino[0] += 0.3;
                angleLeg2_dino[0] -= 0.3;
                angleArm1_dino[0] -= 0.3;
                angleArm2_dino[0] += 0.3;
            }
            else
            {
                angleHead_dino[0] -= 0.1;
                angleLeg1_dino[0] -= 0.3;
                angleLeg2_dino[0] += 0.3;
                angleArm1_dino[0] += 0.3;
                angleArm2_dino[0] -= 0.3;
            }

            if (images_direction[0] == right)
            {
                imagesDino1[head].RenderTransform = new RotateTransform(angleHead_dino[0], 1360 / 8, 1356 / 8);

                imagesDino1[arm1].RenderTransform = new RotateTransform(angleArm1_dino[0], 1475 / 8, 1771 / 8);

                imagesDino1[leg1].RenderTransform = new RotateTransform(angleLeg1_dino[0], 1288 / 8, 2136 / 8);

                imagesDino1[arm2].RenderTransform = new RotateTransform(angleArm1_dino[0], 1152 / 8, 1784 / 8);

                imagesDino1[leg2].RenderTransform = new RotateTransform(angleLeg2_dino[0], 904 / 8, 1944 / 8);
			}
            else
            {
                imagesDino1[head].RenderTransform = new RotateTransform(angleHead_dino[0], 1153 / 8, 1354 / 8);

                imagesDino1[arm1].RenderTransform = new RotateTransform(angleArm1_dino[0], 1008 / 8, 1759 / 8);

                imagesDino1[leg1].RenderTransform = new RotateTransform(angleLeg1_dino[0], 1187 / 8, 2130 / 8);

                imagesDino1[arm2].RenderTransform = new RotateTransform(angleArm1_dino[0], 1321 / 8, 1782 / 8);

                imagesDino1[leg2].RenderTransform = new RotateTransform(angleLeg2_dino[0], 1750 / 8, 1945 / 8);
            }
        }

        private void Timer_timer_UpdateRotate2(object sender, EventArgs e)
        {
            if (imagesDino1[0].Margin.Left > 1920)
            {
                images_direction[0] = left;

                imagesDino1[head].Source = head_flipPartDino1;
                imagesDino1[arm1].Source = arm1_flipPartDino1;
                imagesDino1[leg1].Source = leg1_flipPartDino1;
                imagesDino1[body].Source = body_flipPartDino1;
                imagesDino1[arm2].Source = arm2_flipPartDino1;
                imagesDino1[leg2].Source = leg2_flipPartDino1;

                imagesDino1[head].Margin = new System.Windows.Thickness(1920, imagesDino1[head].Margin.Top, 0, 0);
                imagesDino1[arm1].Margin = new System.Windows.Thickness(1920, imagesDino1[arm1].Margin.Top, 0, 0);
                imagesDino1[leg1].Margin = new System.Windows.Thickness(1920, imagesDino1[leg1].Margin.Top, 0, 0);
                imagesDino1[body].Margin = new System.Windows.Thickness(1920, imagesDino1[body].Margin.Top, 0, 0);
                imagesDino1[arm2].Margin = new System.Windows.Thickness(1920, imagesDino1[arm2].Margin.Top, 0, 0);
                imagesDino1[leg2].Margin = new System.Windows.Thickness(1920, imagesDino1[leg2].Margin.Top, 0, 0);
            }
            else if (imagesDino1[0].Margin.Left + imagesDino1[0].Width < 0)
            {
                images_direction[0] = right;

                imagesDino1[head].Source = headPartDino1;
                imagesDino1[arm1].Source = arm1PartDino1;
                imagesDino1[leg1].Source = leg1PartDino1;
                imagesDino1[body].Source = bodyPartDino1;
                imagesDino1[arm2].Source = arm2PartDino1;
                imagesDino1[leg2].Source = leg2PartDino1;

                imagesDino1[head].Margin = new System.Windows.Thickness(-imagesDino1[head].Width, imagesDino1[head].Margin.Top, 0, 0);
                imagesDino1[arm1].Margin = new System.Windows.Thickness(-imagesDino1[arm1].Width, imagesDino1[arm1].Margin.Top, 0, 0);
                imagesDino1[leg1].Margin = new System.Windows.Thickness(-imagesDino1[leg1].Width, imagesDino1[leg1].Margin.Top, 0, 0);
                imagesDino1[body].Margin = new System.Windows.Thickness(-imagesDino1[body].Width, imagesDino1[body].Margin.Top, 0, 0);
                imagesDino1[arm2].Margin = new System.Windows.Thickness(-imagesDino1[arm2].Width, imagesDino1[arm2].Margin.Top, 0, 0);
                imagesDino1[leg2].Margin = new System.Windows.Thickness(-imagesDino1[leg2].Width, imagesDino1[leg2].Margin.Top, 0, 0);
            }

            if (images_direction[0] == right)
            {
                imagesDino1[head].Margin = new System.Windows.Thickness(imagesDino1[head].Margin.Left + 1, imagesDino1[head].Margin.Top, 0, 0);
                imagesDino1[arm1].Margin = new System.Windows.Thickness(imagesDino1[arm1].Margin.Left + 1, imagesDino1[arm1].Margin.Top, 0, 0);
                imagesDino1[leg1].Margin = new System.Windows.Thickness(imagesDino1[leg1].Margin.Left + 1, imagesDino1[leg1].Margin.Top, 0, 0);
                imagesDino1[body].Margin = new System.Windows.Thickness(imagesDino1[body].Margin.Left + 1, imagesDino1[body].Margin.Top, 0, 0);
                imagesDino1[arm2].Margin = new System.Windows.Thickness(imagesDino1[arm2].Margin.Left + 1, imagesDino1[arm2].Margin.Top, 0, 0);
                imagesDino1[leg2].Margin = new System.Windows.Thickness(imagesDino1[leg2].Margin.Left + 1, imagesDino1[leg2].Margin.Top, 0, 0);
            }
            else
            {

                imagesDino1[head].Margin = new System.Windows.Thickness(imagesDino1[head].Margin.Left - 1, imagesDino1[head].Margin.Top, 0, 0);
                imagesDino1[arm1].Margin = new System.Windows.Thickness(imagesDino1[arm1].Margin.Left - 1, imagesDino1[arm1].Margin.Top, 0, 0);
                imagesDino1[leg1].Margin = new System.Windows.Thickness(imagesDino1[leg1].Margin.Left - 1, imagesDino1[leg1].Margin.Top, 0, 0);
                imagesDino1[body].Margin = new System.Windows.Thickness(imagesDino1[body].Margin.Left - 1, imagesDino1[body].Margin.Top, 0, 0);
                imagesDino1[arm2].Margin = new System.Windows.Thickness(imagesDino1[arm2].Margin.Left - 1, imagesDino1[arm2].Margin.Top, 0, 0);
                imagesDino1[leg2].Margin = new System.Windows.Thickness(imagesDino1[leg2].Margin.Left - 1, imagesDino1[leg2].Margin.Top, 0, 0);
            }
        }

        private void Timer_timer_UpdateRotate3(object sender, EventArgs e)
        {
            if (angleHead_dino[1] > 3)
                angle_direction[1] = left;

            else if (angleHead_dino[1] < -3)
                angle_direction[1] = right;

            if (angle_direction[1] == right)
            {
                angleHead_dino[1] += 0.1;
                angleLeg1_dino[1] += 0.3;
                angleLeg2_dino[1] -= 0.3;
                angleArm1_dino[1] -= 0.3;
                angleArm2_dino[1] += 0.3;
            }
            else
            {
                angleHead_dino[1] -= 0.1;
                angleLeg1_dino[1] -= 0.3;
                angleLeg2_dino[1] += 0.3;
                angleArm1_dino[1] += 0.3;
                angleArm2_dino[1] -= 0.3;
            }

            if (images_direction[1] == right)
            {
                imagesDino2[head].RenderTransform = new RotateTransform(angleHead_dino[1], 1360 / 8, 1356 / 8);

                imagesDino2[arm1].RenderTransform = new RotateTransform(angleArm1_dino[1], 1475 / 8, 1771 / 8);

                imagesDino2[leg1].RenderTransform = new RotateTransform(angleLeg1_dino[1], 1288 / 8, 2136 / 8);

                imagesDino2[arm2].RenderTransform = new RotateTransform(angleArm1_dino[1], 1152 / 8, 1784 / 8);

                imagesDino2[leg2].RenderTransform = new RotateTransform(angleLeg2_dino[1], 904 / 8, 1944 / 8);
			}
            else
            {
                imagesDino2[head].RenderTransform = new RotateTransform(angleHead_dino[1], 1153 / 8, 1354 / 8);

                imagesDino2[arm1].RenderTransform = new RotateTransform(angleArm1_dino[1], 1008 / 8, 1759 / 8);

                imagesDino2[leg1].RenderTransform = new RotateTransform(angleLeg1_dino[1], 1187 / 8, 2130 / 8);

                imagesDino2[arm2].RenderTransform = new RotateTransform(angleArm1_dino[1], 1321 / 8, 1782 / 8);

                imagesDino2[leg2].RenderTransform = new RotateTransform(angleLeg2_dino[1], 1750 / 8, 1945 / 8);
            }
        }

        private void Timer_timer_UpdateRotate4(object sender, EventArgs e)
        {
            if (imagesDino2[0].Margin.Left > 1920)
            {
                images_direction[1] = left;

                imagesDino2[head].Source = head_flipPartDino2;
                imagesDino2[arm1].Source = arm1_flipPartDino2;
                imagesDino2[leg1].Source = leg1_flipPartDino2;
                imagesDino2[body].Source = body_flipPartDino2;
                imagesDino2[arm2].Source = arm2_flipPartDino2;
                imagesDino2[leg2].Source = leg2_flipPartDino2;

                imagesDino2[head].Margin = new System.Windows.Thickness(1920, imagesDino2[head].Margin.Top, 0, 0);
                imagesDino2[arm1].Margin = new System.Windows.Thickness(1920, imagesDino2[arm1].Margin.Top, 0, 0);
                imagesDino2[leg1].Margin = new System.Windows.Thickness(1920, imagesDino2[leg1].Margin.Top, 0, 0);
                imagesDino2[body].Margin = new System.Windows.Thickness(1920, imagesDino2[body].Margin.Top, 0, 0);
                imagesDino2[arm2].Margin = new System.Windows.Thickness(1920, imagesDino2[arm2].Margin.Top, 0, 0);
                imagesDino2[leg2].Margin = new System.Windows.Thickness(1920, imagesDino2[leg2].Margin.Top, 0, 0);
            }
            else if (imagesDino2[0].Margin.Left + imagesDino2[0].Width < 0)
            {
                images_direction[1] = right;

                imagesDino2[head].Source = headPartDino2;
                imagesDino2[arm1].Source = arm1PartDino2;
                imagesDino2[leg1].Source = leg1PartDino2;
                imagesDino2[body].Source = bodyPartDino2;
                imagesDino2[arm2].Source = arm2PartDino2;
                imagesDino2[leg2].Source = leg2PartDino2;

                imagesDino2[head].Margin = new System.Windows.Thickness(-imagesDino2[head].Width, imagesDino2[head].Margin.Top, 0, 0);
                imagesDino2[arm1].Margin = new System.Windows.Thickness(-imagesDino2[arm1].Width, imagesDino2[arm1].Margin.Top, 0, 0);
                imagesDino2[leg1].Margin = new System.Windows.Thickness(-imagesDino2[leg1].Width, imagesDino2[leg1].Margin.Top, 0, 0);
                imagesDino2[body].Margin = new System.Windows.Thickness(-imagesDino2[body].Width, imagesDino2[body].Margin.Top, 0, 0);
                imagesDino2[arm2].Margin = new System.Windows.Thickness(-imagesDino2[arm2].Width, imagesDino2[arm2].Margin.Top, 0, 0);
                imagesDino2[leg2].Margin = new System.Windows.Thickness(-imagesDino2[leg2].Width, imagesDino2[leg2].Margin.Top, 0, 0);
            }

            if (images_direction[1] == right)
            {
                imagesDino2[head].Margin = new System.Windows.Thickness(imagesDino2[head].Margin.Left + 1, imagesDino2[head].Margin.Top, 0, 0);
                imagesDino2[arm1].Margin = new System.Windows.Thickness(imagesDino2[arm1].Margin.Left + 1, imagesDino2[arm1].Margin.Top, 0, 0);
                imagesDino2[leg1].Margin = new System.Windows.Thickness(imagesDino2[leg1].Margin.Left + 1, imagesDino2[leg1].Margin.Top, 0, 0);
                imagesDino2[body].Margin = new System.Windows.Thickness(imagesDino2[body].Margin.Left + 1, imagesDino2[body].Margin.Top, 0, 0);
                imagesDino2[arm2].Margin = new System.Windows.Thickness(imagesDino2[arm2].Margin.Left + 1, imagesDino2[arm2].Margin.Top, 0, 0);
                imagesDino2[leg2].Margin = new System.Windows.Thickness(imagesDino2[leg2].Margin.Left + 1, imagesDino2[leg2].Margin.Top, 0, 0);
            }
            else
            {

                imagesDino2[head].Margin = new System.Windows.Thickness(imagesDino2[head].Margin.Left - 1, imagesDino2[head].Margin.Top, 0, 0);
                imagesDino2[arm1].Margin = new System.Windows.Thickness(imagesDino2[arm1].Margin.Left - 1, imagesDino2[arm1].Margin.Top, 0, 0);
                imagesDino2[leg1].Margin = new System.Windows.Thickness(imagesDino2[leg1].Margin.Left - 1, imagesDino2[leg1].Margin.Top, 0, 0);
                imagesDino2[body].Margin = new System.Windows.Thickness(imagesDino2[body].Margin.Left - 1, imagesDino2[body].Margin.Top, 0, 0);
                imagesDino2[arm2].Margin = new System.Windows.Thickness(imagesDino2[arm2].Margin.Left - 1, imagesDino2[arm2].Margin.Top, 0, 0);
                imagesDino2[leg2].Margin = new System.Windows.Thickness(imagesDino2[leg2].Margin.Left - 1, imagesDino2[leg2].Margin.Top, 0, 0);

            }
        }

        private void Timer_timer_UpdateRotate5(object sender, EventArgs e)
        {
        }

        private void Timer_timer_UpdateRotate6(object sender, EventArgs e)
        {
        }

        private void Timer_Tick_UpdateFile(object sender, EventArgs e)
        {
            int fileCount = Directory.GetFiles(PathImagesDinoShow).Length;
            if (fileCount > countTemp)
            {
                string[] files = Directory.GetFiles(PathImagesDinoShow);
                List<FileList> objFileList = new List<FileList>();
                foreach (string file in files)
                {
                    DateTime creationTime = System.IO.File.GetCreationTime(file);
                    objFileList.Add(new FileList(file, creationTime));
                    //Console.WriteLine(System.IO.Path.GetFileName(file));
                }

                Console.WriteLine("Sort the list by date descending:");
                objFileList.Sort((x, y) => y.FileDate.CompareTo(x.FileDate));

                try
                {

                    string[] str = objFileList[0].FileName.Split('\\');
                    str = str[str.Length - 1].Split('.');
                    DinoFileName = str[0];

                    if (count == 0)
                    {
                        //image
                        leg1PartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg1.png"));
                        leg2PartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg2.png"));
                        bodyPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "body.png"));
                        arm1PartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm1.png"));
                        arm2PartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm2.png"));
                        headPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "head.png"));
                        //flip image
                        leg1_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg1_flip.png"));
                        leg2_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg2_flip.png"));
                        body_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "body_flip.png"));
                        arm1_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm1_flip.png"));
                        arm2_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm2_flip.png"));
                        head_flipPartDino1 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "head_flip.png"));

                        //set image
                        locationX_dino[0] = 0;
                        locationY_dino[0] = 400;

                        //set angle
                        angleHead_dino[0] = 0;
                        angleBody_dino[0] = 0;
                        angleArm1_dino[0] = 0;
                        angleArm2_dino[0] = 0;
                        angleLeg1_dino[0] = 0;
                        angleLeg2_dino[0] = 0;

                        //dino set image
                        //head
                        imagesDino1[head].Width = imageDino_width;
                        imagesDino1[head].Height = imageDino_height;
                        imagesDino1[head].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[head].Source = headPartDino1;
                        imagesDino1[head].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);
                        //arm1
                        imagesDino1[arm1].Width = imageDino_width;
                        imagesDino1[arm1].Height = imageDino_height;
                        imagesDino1[arm1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[arm1].Source = arm1PartDino1;
                        imagesDino1[arm1].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);
                        //leg1
                        imagesDino1[leg1].Width = imageDino_width;
                        imagesDino1[leg1].Height = imageDino_height;
                        imagesDino1[leg1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[leg1].Source = leg1PartDino1;
                        imagesDino1[leg1].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);
                        //body
                        imagesDino1[body].Width = imageDino_width;
                        imagesDino1[body].Height = imageDino_height;
                        imagesDino1[body].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[body].Source = bodyPartDino1;
                        imagesDino1[body].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);
                        //arm2
                        imagesDino1[arm2].Width = imageDino_width;
                        imagesDino1[arm2].Height = imageDino_height;
                        imagesDino1[arm2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[arm2].Source = arm2PartDino1;
                        imagesDino1[arm2].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);

                        //leg2
                        imagesDino1[leg2].Width = imageDino_width;
                        imagesDino1[leg2].Height = imageDino_height;
                        imagesDino1[leg2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino1[leg2].Source = leg2PartDino1;
                        imagesDino1[leg2].Margin = new System.Windows.Thickness(locationX_dino[0], locationY_dino[0], 0, 0);

                        //Dino1 add
                        ContentRoot.Children.Add(imagesDino1[0]);
                        ContentRoot.Children.Add(imagesDino1[1]);
                        ContentRoot.Children.Add(imagesDino1[2]);
                        ContentRoot.Children.Add(imagesDino1[3]);
                        ContentRoot.Children.Add(imagesDino1[4]);
                        ContentRoot.Children.Add(imagesDino1[5]);

                        //Redraw tree 
                        ContentRoot.Children.Remove(imagesEnvironment[0]);
                        ContentRoot.Children.Remove(imagesEnvironment[2]);
                        //add
                        ContentRoot.Children.Add(imagesEnvironment[0]);
                        ContentRoot.Children.Add(imagesEnvironment[2]);

                        timer_UpdateRotate[0].Start();
                        timer_UpdateRotate[1].Start();
                    }
                    else if (count == 1)
                    {
                        //image
                        leg1PartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg1.png"));
                        leg2PartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg2.png"));
                        bodyPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "body.png"));
                        arm1PartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm1.png"));
                        arm2PartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm2.png"));
                        headPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "head.png"));
                        //flip image
                        leg1_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg1_flip.png"));
                        leg2_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "leg2_flip.png"));
                        body_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "body_flip.png"));
                        arm1_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm1_flip.png"));
                        arm2_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "arm2_flip.png"));
                        head_flipPartDino2 = new BitmapImage(new Uri(PathImagesDinoUse + DinoFileName + "head_flip.png"));

                        //set image
                        locationX_dino[1] = 0;
                        locationY_dino[1] = 200;

                        //set angle
                        angleHead_dino[1] = 0;
                        angleBody_dino[1] = 0;
                        angleArm1_dino[1] = 0;
                        angleArm2_dino[1] = 0;
                        angleLeg1_dino[1] = 0;
                        angleLeg2_dino[1] = 0;

                        //dino set image
                        //head
                        imagesDino2[head].Width = imageDino_width;
                        imagesDino2[head].Height = imageDino_height;
                        imagesDino2[head].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[head].Source = headPartDino2;
                        imagesDino2[head].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);
                        //arm1
                        imagesDino2[arm1].Width = imageDino_width;
                        imagesDino2[arm1].Height = imageDino_height;
                        imagesDino2[arm1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[arm1].Source = arm1PartDino2;
                        imagesDino2[arm1].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);
                        //leg1
                        imagesDino2[leg1].Width = imageDino_width;
                        imagesDino2[leg1].Height = imageDino_height;
                        imagesDino2[leg1].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[leg1].Source = leg1PartDino2;
                        imagesDino2[leg1].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);
                        //body
                        imagesDino2[body].Width = imageDino_width;
                        imagesDino2[body].Height = imageDino_height;
                        imagesDino2[body].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[body].Source = bodyPartDino2;
                        imagesDino2[body].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);
                        //arm2
                        imagesDino2[arm2].Width = imageDino_width;
                        imagesDino2[arm2].Height = imageDino_height;
                        imagesDino2[arm2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[arm2].Source = arm2PartDino2;
                        imagesDino2[arm2].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);

                        //leg2
                        imagesDino2[leg2].Width = imageDino_width;
                        imagesDino2[leg2].Height = imageDino_height;
                        imagesDino2[leg2].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        imagesDino2[leg2].Source = leg2PartDino2;
                        imagesDino2[leg2].Margin = new System.Windows.Thickness(locationX_dino[1], locationY_dino[1], 0, 0);

                        //Dino1 add
                        ContentRoot.Children.Add(imagesDino2[0]);
                        ContentRoot.Children.Add(imagesDino2[1]);
                        ContentRoot.Children.Add(imagesDino2[2]);
                        ContentRoot.Children.Add(imagesDino2[3]);
                        ContentRoot.Children.Add(imagesDino2[4]);
                        ContentRoot.Children.Add(imagesDino2[5]);

                        //Redraw Dino1 
                        ContentRoot.Children.Remove(imagesDino1[0]);
                        ContentRoot.Children.Remove(imagesDino1[1]);
                        ContentRoot.Children.Remove(imagesDino1[2]);
                        ContentRoot.Children.Remove(imagesDino1[3]);
                        ContentRoot.Children.Remove(imagesDino1[4]);
                        ContentRoot.Children.Remove(imagesDino1[5]);
                        //Dino1 add
                        ContentRoot.Children.Add(imagesDino1[0]);
                        ContentRoot.Children.Add(imagesDino1[1]);
                        ContentRoot.Children.Add(imagesDino1[2]);
                        ContentRoot.Children.Add(imagesDino1[3]);
                        ContentRoot.Children.Add(imagesDino1[4]);
                        ContentRoot.Children.Add(imagesDino1[5]);

                        //Redraw tree 
                        ContentRoot.Children.Remove(imagesEnvironment[0]);
                        ContentRoot.Children.Remove(imagesEnvironment[2]);
                        //add
                        ContentRoot.Children.Add(imagesEnvironment[0]);
                        ContentRoot.Children.Add(imagesEnvironment[2]);

                        timer_UpdateRotate[2].Start();
                        timer_UpdateRotate[3].Start();
                    }
                    count++;
                    if (count == 10) count = 0;
                    Console.WriteLine("Count = " + count.ToString(), "Count");
                    countTemp = fileCount;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception = " + ex.ToString());
                }
            }
            Console.WriteLine("FileCount = " + fileCount,"FileCount");
        }

        private void Timer_Tick_UpdateCharacters(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                if (imagesDino1[i].Margin.Left + imagesDino1[i].Width == 1366)
                    images_direction[i] = left;
                else if(imagesDino1[i].Margin.Left == 0)
                    images_direction[i] = right;

                if (images_direction[i] == right)
                    imagesDino1[i].Margin = new System.Windows.Thickness(imagesDino1[i].Margin.Left + 1, imagesDino1[i].Margin.Top, 0, 0);
                else
                    imagesDino1[i].Margin = new System.Windows.Thickness(imagesDino1[i].Margin.Left - 1, imagesDino1[i].Margin.Top, 0, 0);
            }
        }

        static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        static List<DateTime> SortDescending(List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        static void Display(List<DateTime> list, string message)
        {
            Console.WriteLine(message);
            foreach (var datetime in list)
            {
                Console.WriteLine(datetime);
            }
            Console.WriteLine();
        }
    }

    public class FileList
    {
        public string FileName { get; set; }
        public DateTime FileDate { get; set; }

        public FileList(string filename, DateTime filedate)
        {
            FileName = filename;
            FileDate = filedate;
        }
    }
}




