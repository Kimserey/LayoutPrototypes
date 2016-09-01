using System;

using Xamarin.Forms;

namespace LayoutPrototypes
{
	public static class Extensions { 
		public static void AddBox(this AbsoluteLayout layout, Color color, double x, double y)
		{
			var box = new BoxView { BackgroundColor = color, };
			layout.Children.Add(box, new Rectangle(x, y, .25, .25), AbsoluteLayoutFlags.All);
		}
	}

	public class AbsoluteLayoutTest : ContentPage
	{

		public AbsoluteLayoutTest()
		{
			var layout =  new AbsoluteLayout();
			layout.AddBox(Color.Blue, 0, 0);
			layout.AddBox(Color.Fuchsia, 0.25, 0.25);
			layout.AddBox(Color.Gray, 0.5, 0.5);
			layout.AddBox(Color.Yellow, 0.75, 0.75);
			layout.AddBox(Color.Green, 1, 1);
			this.Content = layout;
		}
	}
}


