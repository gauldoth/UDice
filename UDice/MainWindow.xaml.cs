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
using System.Windows.Media.Effects;

namespace UDice
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();



		}

		private void minButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void titlePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private double GetDistance(Point a, Point b)
		{
			double deltaX = a.X - b.X;
			double deltaY = a.Y - b.Y;
			return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
		}

		private double GetDistance(double x1, double y1, double x2, double y2)
		{
			double deltaX = x2 - x1;
			double deltaY = y2 - y1;
			return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
		}

		private double GetRadian(Point a, Point b)
		{
			double radius = GetDistance(a, b);
			double deltaX = b.X - a.X;

			return Math.Atan2(b.Y - a.Y,b.X - a.X);
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			//创建硬币,并放入tossACoinPanel.
			coin = new Coin();
			coin.X = tossACoinCanvas.ActualWidth / 2;
			coin.Y = tossACoinCanvas.ActualHeight / 2;
			tossACoinCanvas.Children.Add(coin);

			System.Windows.Threading.DispatcherTimer tossACoinTimer = 
				new System.Windows.Threading.DispatcherTimer();
			tossACoinTimer.Tick += new EventHandler(tossACoinTimer_Elapsed);
			tossACoinTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
			tossACoinTimer.Start();

			aura.Visibility = Visibility.Hidden;
			RadialGradientBrush auraBrush = new RadialGradientBrush();
			auraBrush.GradientOrigin = new Point(0.5, 0.5);
			auraBrush.Center = new Point(0.5, 0.5);

			GradientStop centerGS = new GradientStop();
			centerGS.Color = Colors.Green;
			centerGS.Offset = 0.0;
			auraBrush.GradientStops.Add(centerGS);

			GradientStop borderGS = new GradientStop();
			borderGS.Color = Colors.Red;
			borderGS.Offset = 1.5;
			auraBrush.GradientStops.Add(borderGS);
			aura.Fill = auraBrush;

			//aura.Fill = new GradientBrush();
			BlurEffect effect = new BlurEffect();
			effect.Radius = 5;
			aura.Effect = effect;
			tossACoinCanvas.Children.Add(aura);
		}

		int maxSpirit = 0;
		double gatheredSpiritPower = 0;
		List<Shape> spirits = new List<Shape>();
		Shape aura = new Ellipse();
		private void tossACoinTimer_Elapsed(object sender, EventArgs e)
		{


			if (tossing)
			{
				//coin.Flip += flipVelocity;
				coin.X += xVelocity;
				coin.Y += yVelocity;
				zVelocity += zAcceleration;
				coin.Altitude += zVelocity;
				if (coin.Altitude < 0)
				{
					coin.Altitude = 0;
					tossing = false;
				}
			}

			if (mouseLeftButtonDown)
			{
				if (maxSpirit < 120)
				{
					maxSpirit++;
				}
				if (spirits.Count < maxSpirit)
				{
					//获得中点附近的一个点.
					double radius = rand.NextDouble()*50+30;
					double radian = rand.NextDouble() * 2 * Math.PI;
					double xOffset = Math.Cos(radian)*radius;
					double yOffset = Math.Sin(radian)*radius;
					
					Ellipse ellipse = new Ellipse();
					ellipse.Fill = new SolidColorBrush(Colors.LightSeaGreen);
					ellipse.Width = 5.0;
					ellipse.Height = 5.0;
					tossACoinCanvas.Children.Add(ellipse);
					Canvas.SetLeft(ellipse,powerCenter.X + xOffset - 2.5);
					Canvas.SetTop(ellipse,powerCenter.Y + yOffset - 2.5);
					spirits.Add(ellipse);
				}
			}

			for (int i = 0; i < spirits.Count; i++)
			{
				//向当前的PowerCenter移动.
				Shape spirit = spirits[i];
				double x = Canvas.GetLeft(spirit) + 2.5;
				double y = Canvas.GetTop(spirit) + 2.5;
				Point spiritCenter = new Point(x, y);
				double distance = GetDistance(spiritCenter, powerCenter);
				double speed = distance * 0.05 + 2;
				if (speed > distance)
				{
					speed = distance;
					tossACoinCanvas.Children.Remove(spirit);
					spirits.RemoveAt(i);
					i--;
					gatheredSpiritPower += 5;
				}
				double radian = GetRadian(spiritCenter, powerCenter);
				double newX = x + speed * Math.Cos(radian);
				double newY = y + speed * Math.Sin(radian);
				Canvas.SetLeft(spirit, newX - 2.5);
				Canvas.SetTop(spirit, newY - 2.5);

			}

			double auraRadius = gatheredSpiritPower / 5;
			aura.Width = auraRadius;
			aura.Height = auraRadius;
			aura.Opacity = 0.2 + gatheredSpiritPower/500;
			Canvas.SetLeft(aura, powerCenter.X - auraRadius / 2);
			Canvas.SetTop(aura, powerCenter.Y - auraRadius / 2);


			coin.UpdateAppearance();

			gatheredSpiritPower -= gatheredSpiritPower / 100;
			if (gatheredSpiritPower < 0)
			{
				gatheredSpiritPower = 0;
			}

		}

		private Coin coin;
		private Random rand = new Random();
		private bool mouseLeftButtonDown = false;
		private bool tossing = false;
		private double xVelocity = 0;
		private double yVelocity = 0;
		private double zVelocity = 0;
		private double zAcceleration = 0;
		private void tossACoinCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//开始计算按下时间.
			mouseLeftButtonDown = true;
			aura.Visibility = Visibility.Visible;
			powerCenter = e.GetPosition(tossACoinCanvas);
		}

		private void tossACoinCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//计算路径.
			mouseLeftButtonDown = false;

			//计算是否有碰撞发生.
			powerCenter = e.GetPosition(tossACoinCanvas);
			double distanceFromCenter = GetDistance(coin.X,coin.Y,powerCenter.X,powerCenter.Y);
			if (distanceFromCenter < coin.Radius && coin.Altitude == 0)
			{
				tossing = true;

				//计算水平方向和速度.
				xVelocity = (coin.X - powerCenter.X) * gatheredSpiritPower/500;
				yVelocity = (coin.Y - powerCenter.Y) * gatheredSpiritPower/500;

				//计算Z方向的加速度为重力.
				zAcceleration = -0.8;

				zVelocity = gatheredSpiritPower / 30;

				gatheredSpiritPower = 0;
			}

			popLink.IsOpen = true;
		}

		private Point powerCenter;
		private void tossACoinCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseLeftButtonDown)
			{
				powerCenter = e.GetPosition(tossACoinCanvas);
			}
		}
	}
}
