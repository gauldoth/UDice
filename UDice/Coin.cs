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
			Angle = 0;

			shadowEffect = new DropShadowEffect();
			shadowEffect.BlurRadius = 5;
			shadowEffect.ShadowDepth = Altitude+1;
			//this.Effect = shadowEffect;

			ellipse = new Ellipse();
			

			ellipse2 = new Ellipse();

			Grid grid = new Grid();
			grid.Children.Add(ellipse);
			grid.Children.Add(ellipse2);
			this.Content = grid;

			mainBrush = new SolidColorBrush(Colors.Silver);
			borderBrush = new SolidColorBrush(Colors.DarkGray);
			backBrush = new SolidColorBrush(Colors.SlateGray);
			ellipse.Fill = mainBrush;
			ellipse.Stroke = borderBrush;
			ellipse.Stretch = Stretch.Fill;
			ellipse2.Fill = backBrush;
			ellipse2.Stroke = borderBrush;
			ellipse2.Stretch = Stretch.Fill;

			TransformGroup transformGroup = new TransformGroup();
			scaleTransform = new ScaleTransform();
			transformGroup.Children.Add(scaleTransform);
			rotateTransform = new RotateTransform();
			transformGroup.Children.Add(rotateTransform);

			ellipse.RenderTransform = transformGroup;
			ellipse.RenderTransformOrigin = new Point(0.5, 0.5);

			UpdateAppearance();
		}

		public double Altitude { get; set; }

		public double X { get; set; }

		public double Y { get; set; }

		//与水平方向的夹角,弧度为单位.
		public double Angle { get; set; }

		//与X轴的夹角.
		public double Angle2 { get; set; }

		public double Radius { get; set; }

		public void UpdateAppearance()
		{
			this.Width = Radius*2 + 0.5 * Altitude;
			this.Height = Radius*2 + 0.5 * Altitude;

			shadowEffect.ShadowDepth = Altitude+1;

			Canvas.SetLeft(this, X - this.Width/2);
			Canvas.SetTop(this, Y- this.Height/2);

			scaleTransform.ScaleX = Math.Cos(Angle);
			rotateTransform.Angle = Angle2 * 180 / Math.PI;
		}

		private Ellipse ellipse;
		private Ellipse ellipse2;
		private SolidColorBrush mainBrush;
		private SolidColorBrush backBrush;
		private SolidColorBrush borderBrush;
		private RotateTransform rotateTransform;
		private ScaleTransform scaleTransform;
	}
}
