using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class TagView : ContentView
	{
		public TagView()
		{
			this.Padding = new Thickness(4, 2);
			this.Content = new Label { Text = "Hello", TextColor = Color.White, FontSize = 10, FontAttributes = FontAttributes.Bold };
			this.BackgroundColor = Color.Purple;
		}
	}

	public class Cell : ViewCell
	{
		private Label title = new Label();
		private StackLayout layout = new StackLayout { Padding = new Thickness(5) };
		private StackLayout horizontalLayout =
			new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.Start
			};
		private ScrollView scroll = new ScrollView { Orientation = ScrollOrientation.Horizontal };

		public Cell()
		{
			title.Text = "Hello world";
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			horizontalLayout.Children.Add(new TagView());
			scroll.Content = horizontalLayout;
			layout.Children.Add(title);
			layout.Children.Add(scroll);
			this.View = layout;
		}
	}

	public class ListViewPage : ContentPage
	{
		private ListView list = new ListView();

		public ListViewPage()
		{
			this.Title = "List view";

			list.ItemsSource = Enumerable.Range(0, 100);
			list.ItemTemplate = new DataTemplate(typeof(Cell));
			list.RowHeight = 55;
			this.Content = list;
		}
	}
}
