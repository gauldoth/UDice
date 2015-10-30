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
			tossACoinTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			tossACoinTimer.Start();

		}

		private void tossACoinTimer_Elapsed(object sender, EventArgs e)
		{
			coin.Altitude += 1;
			coin.X += 0.5;
			coin.Y += 0.5;
			coin.UpdateAppearance();
		}

		private Coin coin;

		private void tossACoinCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//开始计算按下时间.
		}

		private void tossACoinCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//计算路径.
		}
	}
}
