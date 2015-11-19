using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Threading;

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

			center = new Border();

			ellipse2 = new Ellipse();

			Grid grid = new Grid();
			grid.Children.Add(ellipse);
			grid.Children.Add(center);
			grid.Children.Add(ellipse2);
			Grid.SetZIndex(ellipse, 2);
			Grid.SetZIndex(center, 1);
			Grid.SetZIndex(ellipse2, 0);
			this.Content = grid;

			mainBrush = new SolidColorBrush(Colors.Silver);
			borderBrush = new SolidColorBrush(Colors.Black);
			backBrush = new SolidColorBrush(Colors.SlateGray);
			sideBrush = new SolidColorBrush(Colors.DarkGray);

			ellipse.Fill = mainBrush;
			ellipse.Stroke = borderBrush;
			ellipse.StrokeThickness = 0.5;
			ellipse.Stretch = Stretch.Fill;

			Rectangle centerRect = new Rectangle();
			center.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
			center.BorderBrush = borderBrush;
			center.Child = centerRect;
			centerRect.Fill = sideBrush;
			

			ellipse2.Fill = backBrush;
			ellipse2.Stroke = borderBrush;
			ellipse2.StrokeThickness = 0.5;
			ellipse2.Stretch = Stretch.Fill;

			TransformGroup transformGroup = new TransformGroup();

			scaleTransform = new ScaleTransform();
			transformGroup.Children.Add(scaleTransform);
			translateTransform = new TranslateTransform();
			transformGroup.Children.Add(translateTransform);
			rotateTransform = new RotateTransform();
			transformGroup.Children.Add(rotateTransform);


			TransformGroup transformGroup2 = new TransformGroup();

			scaleTransform2 = new ScaleTransform();
			transformGroup2.Children.Add(scaleTransform2);
			translateTransform2 = new TranslateTransform();
			transformGroup2.Children.Add(translateTransform2);
			rotateTransform2 = new RotateTransform();
			transformGroup2.Children.Add(rotateTransform2);

			TransformGroup transformGroupC = new TransformGroup();
			scaleTransformC = new ScaleTransform();
			rotateTransformC = new RotateTransform();
			transformGroupC.Children.Add(scaleTransformC);
			transformGroupC.Children.Add(rotateTransformC);



			ellipse.RenderTransform = transformGroup;
			ellipse.RenderTransformOrigin = new Point(0.5, 0.5);

			ellipse2.RenderTransform = transformGroup2;
			ellipse2.RenderTransformOrigin = new Point(0.5, 0.5);

			center.RenderTransform = transformGroupC;
			center.RenderTransformOrigin = new Point(0.5, 0.5);

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

			translateTransform.X = (Altitude/30+2.5) * Math.Cos(Angle + Math.PI / 2);
			scaleTransform.ScaleX = Math.Abs(Math.Cos(Angle));
			rotateTransform.Angle = Angle2 * 180 / Math.PI;

			translateTransform2.X = (Altitude/30+2.5) * Math.Cos(Angle + Math.PI * 3 / 2);
			scaleTransform2.ScaleX = Math.Abs(Math.Cos(Angle));
			rotateTransform2.Angle = Angle2 * 180 / Math.PI;

			scaleTransformC.ScaleX = Math.Abs(translateTransform.X*2)/this.Width;
			rotateTransformC.Angle = Angle2 * 180 / Math.PI;

			double angleIn2PI = Angle % (2 * Math.PI);
			if (angleIn2PI > (Math.PI / 2) && angleIn2PI < (3 * Math.PI / 2))
			{
				Grid.SetZIndex(ellipse, 0);
				Grid.SetZIndex(ellipse2, 2);
				ellipse2.Fill = backBrush;
				ellipse.Fill = sideBrush;
			}
			else
			{
				Grid.SetZIndex(ellipse, 2);
				Grid.SetZIndex(ellipse2, 0);
				ellipse2.Fill = sideBrush;
				ellipse.Fill = mainBrush;
			}
		}

		private Ellipse ellipse;
		private Border center;
		private Ellipse ellipse2;
		private SolidColorBrush mainBrush;
		private SolidColorBrush backBrush;
		private SolidColorBrush borderBrush;
		private SolidColorBrush sideBrush;
		private RotateTransform rotateTransform;
		private ScaleTransform scaleTransform;
		private TranslateTransform translateTransform;
		private RotateTransform rotateTransform2;
		private ScaleTransform scaleTransform2;
		private TranslateTransform translateTransform2;
		private ScaleTransform scaleTransformC;
		private RotateTransform rotateTransformC;
	}
}
