using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace UDice
{
	public class Coin : ContentControl
	{
		DropShadowEffect shadowEffect;
		public Coin()
		{
			Altitude = 0;
			X = 0;
			Y = 0;
			Radius = 15;

			shadowEffect = new DropShadowEffect();
			shadowEffect.BlurRadius = 5;
			shadowEffect.ShadowDepth = Altitude+1;
			this.Effect = shadowEffect;

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
			this.Width = Radius*2 + 0.5 * Altitude;
			this.Height = Radius*2 + 0.5 * Altitude;

			shadowEffect.ShadowDepth = Altitude+1;

			Canvas.SetLeft(this, X - this.Width/2);
			Canvas.SetTop(this, Y- this.Height/2);
		}

		private Ellipse ellipse;
		private SolidColorBrush mainBrush;
		private SolidColorBrush borderBrush;
	}
}
