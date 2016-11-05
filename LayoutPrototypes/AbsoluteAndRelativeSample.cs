using System;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class AbsoluteLayoutTest : ContentPage
	{

		public AbsoluteLayoutTest()
		{
			this.Title = "Absolute";

			var layout = new AbsoluteLayout();
			var box = new BoxView { BackgroundColor = Color.Blue };
			layout.Children.Add(box, new Rectangle(0, 0, .25, .25), AbsoluteLayoutFlags.All);

			var box1 = new BoxView { BackgroundColor = Color.Fuchsia };
			layout.Children.Add(box1, new Rectangle(.25, .25, .25, .25), AbsoluteLayoutFlags.All);

			var box2 = new BoxView { BackgroundColor = Color.Gray };
			layout.Children.Add(box2, new Rectangle(.5, .5, .25, .25), AbsoluteLayoutFlags.All);

			var box3 = new BoxView { BackgroundColor = Color.Yellow };
			layout.Children.Add(box3, new Rectangle(.75, .75, .25, .25), AbsoluteLayoutFlags.All);

			var box4 = new BoxView { BackgroundColor = Color.Green };
			layout.Children.Add(box4, new Rectangle(1, 1, .25, .25), AbsoluteLayoutFlags.All);

			this.Content = layout;
		}
	}

	public class RelativeLayoutTest : ContentPage
	{
		public RelativeLayoutTest()
		{
			this.Title = "Relative";

			var layout = new RelativeLayout();
			var box = new BoxView { BackgroundColor = Color.Blue };
			layout.Children.Add(
				box,
				Constraint.RelativeToParent(parent => parent.X),
				Constraint.RelativeToParent(parent => parent.Y),
				Constraint.RelativeToParent(parent => (parent.Width * 25) / 100),
				Constraint.RelativeToParent(parent => (parent.Height * 25) / 100)
			);

			var box2 = new BoxView { BackgroundColor = Color.Fuchsia };
			layout.Children.Add(
				box2,
				Constraint.RelativeToView(box, (parent, view) => view.X + view.Width),
				Constraint.RelativeToView(box, (parent, view) => view.Y + view.Height),
				Constraint.RelativeToParent(parent => (parent.Width * 25) / 100),
				Constraint.RelativeToParent(parent => (parent.Height * 25) / 100)
			);

			var box3 = new BoxView { BackgroundColor = Color.Gray };
			layout.Children.Add(
				box3,
				Constraint.RelativeToView(box2, (parent, view) => view.X + view.Width),
				Constraint.RelativeToView(box2, (parent, view) => view.Y + view.Height),
				Constraint.RelativeToParent(parent => (parent.Width * 25) / 100),
				Constraint.RelativeToParent(parent => (parent.Height * 25) / 100)
			);

			var box4 = new BoxView { BackgroundColor = Color.Yellow };
			layout.Children.Add(
				box4,
				Constraint.RelativeToView(box3, (parent, view) => view.X + view.Width),
				Constraint.RelativeToView(box3, (parent, view) => view.Y + view.Height),
				Constraint.RelativeToParent(parent => (parent.Width * 25) / 100),
				Constraint.RelativeToParent(parent => (parent.Height * 25) / 100)
			);

			this.Content = layout;
		}
	}

	public class TabPage : TabbedPage
	{
		public TabPage()
		{
			Children.Add(new ListViewPage());
			Children.Add(new AbsoluteLayoutTest());
			Children.Add(new RelativeLayoutTest());
		}
	}
}


