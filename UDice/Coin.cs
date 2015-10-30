using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UDice
{
	public class Coin : ContentControl
	{
		public Coin()
		{
			Altitude = 0;
			X = 0;
			Y = 0;

			ellipse = new Ellipse();
			this.Content = ellipse;

			mainBrush = new SolidColorBrush(Colors.Silver);
			borderBrush = new SolidColorBrush(Colors.Gray);
			ellipse.Fill = mainBrush;
			ellipse.Stroke = borderBrush;
			ellipse.Stretch = Stretch.Fill;

			UpdateAppearance();
		}

		public double Altitude { get; set; }

		public double X { get; set; }

		public double Y { get; set; }

		public double Angle { get; set; }

		public double Radius { get; set; }

		public void UpdateAppearance()
		{
			this.Width = 20.0 + 0.5 * Altitude;
			this.Height = 20.0 + 0.5 * Altitude;

			Canvas.SetLeft(this, X);
			Canvas.SetTop(this, Y);
		}

		private Ellipse ellipse;
		private SolidColorBrush mainBrush;
		private SolidColorBrush borderBrush;
	}
}
